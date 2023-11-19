using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class ContentManager : MonoBehaviour
{
    public GameObject productPrefab;

    private List<Product> products = new List<Product>();

    private string filePath;

    private DateTime updatesDate; //All the updates before this date, will be highlighted

    private Filter filter = null;

    public NotificationPanelController notification;

    private void Start()
    {
        filePath = Application.dataPath + "/Stock.txt";
        if (File.Exists(filePath))
            LoadContent();
        else
        {
            File.Create(filePath);
            updatesDate = DateTime.Now;
        }
    }

    private void LoadContent()
    {
        Debug.Log("Cargando stock.");
        StreamReader sr = new StreamReader(filePath);

        string[] uDate = sr.ReadLine().Split('-');
        updatesDate = new DateTime(int.Parse(uDate[2]), int.Parse(uDate[1]), int.Parse(uDate[0]));

        string p = sr.ReadLine();
        while (p!= null)
        {
            string[] vars = p.Split(';');

            products.Add(new Product(vars[0], vars[1], vars[2], vars[3], int.Parse(vars[4]), double.Parse(vars[5]), double.Parse(vars[6]), vars[7]));
            
            GameObject newP = (GameObject)Instantiate(productPrefab);
            newP.transform.SetParent(this.transform);
            LoadProduct(newP, vars[0], vars[1], vars[2], vars[3], vars[4], vars[5], vars[6], vars[7]);
            newP.transform.localScale = new Vector3(1, 1, 1);

            p = sr.ReadLine();
        }
        sr.Close();
    }

    private void LoadFile()//Maybe it can be used from a button and not for each update
    {
        StreamWriter sw = new StreamWriter(filePath);

        sw.WriteLine(updatesDate.ToString("dd-MM-yyyy"));

        foreach (Product pr in products)
            sw.WriteLine( pr.ToString() );

        sw.Close();
    }

    public void SetUpdatesDate(DateTime newUD)
    {
        if(updatesDate.CompareTo(newUD)!=0)
        {
            updatesDate = newUD;
            ReLoadContent();
            LoadFile();
        }
    }

    public bool AddNewProduct(string code, string name, string brand, string category, string quant, string cost, string price)
    {
        string update = DateTime.Now.ToString("dd-MM-yyyy");
        Product p = new Product(code, name, brand, category, int.Parse(quant), double.Parse(cost), double.Parse(price), update);
        if (products.Contains(p))
        {
            Debug.Log("ERROR: Ya existe el producto: " + code + ", nombre " + name + " de la marca " + brand);
            notification.OpenPanel("ERROR", "Ya existe el producto: " + code + ", " + name + " de la marca " + brand +". \nPor favor modifique los datos o cancele la operación.");
            return false;
        }
        products.Add(p);
        Debug.Log("Se agregó el producto: " + code + ", nombre " + name + " de la marca " + brand);

        if(filter == null || filter.Satisfy(p))
        {
            GameObject newP = (GameObject)Instantiate(productPrefab);
            newP.transform.SetParent(this.transform);
            LoadProduct(newP, code, name, brand, category, quant, cost, price, update);
            newP.transform.localScale = new Vector3(1, 1, 1);
        }

        LoadFile();

        return true;
    }

    public bool UpdateProduct(GameObject p, string code, string name, string brand, string category, string quant, string cost, string price)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        if ( ((vars[0].text != code) //It mean, they're the same keys
            && products.Contains(new Product(code, " ", " ")))
            ||((vars[1].text != name || vars[2].text != brand) //It mean, they're the same secundary keys
            && products.Contains(new Product(" ", name, brand)))
            )
        {
            Debug.Log("ERROR: Ya existe el producto: " + code + ", " + name + " de la marca " + brand);
            notification.OpenPanel("ERROR", "Ya existe el producto: " + code + ", " + name + " de la marca " + brand + "." + "\nPor favor modifique los datos o cancele la operación.");
            return false;
        }

        int posP = products.IndexOf(new Product(vars[0].text));
        products[posP].SetAll(code, name, brand, category, int.Parse(quant), double.Parse(cost), double.Parse(price));
        Debug.Log("Se modificó el producto con código " + vars[0].text);

        if (filter == null || filter.Satisfy(products[posP]))
            LoadProduct(p, code, name, brand, category, quant, cost, price, products[posP].Update);
        else
            Destroy(p);

        LoadFile();

        return true;
    }

    public void DeleteProduct(GameObject p)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        products.Remove(new Product(vars[0].text));
        Destroy(p);

        LoadFile();
    }

    private void LoadProduct(GameObject p, string code, string product, string brand, string category, string quant, string cost, string price, string update)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        vars[0].text = code;
        vars[1].text = product;
        vars[2].text = brand;
        vars[3].text = category;
        vars[4].text = quant;
        vars[5].text = cost;
        vars[6].text = price;

        string[] date = update.Split('-');
        DateTime uDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        if (updatesDate.CompareTo(uDate) <= 0)
            update = "<mark =#ffff00>" + update; //Highlight the date

        vars[7].text = update;
    }

    public void UpdatePriceByFilters(int typeFilter, string filter, int avg)
    {
        avg += 100;
        double average = double.Parse(avg.ToString()) / 100;
        if (typeFilter == 0)//Change all
        {
            foreach (Product pr in products)
                UpdatePrice(pr, average);
        }
        if (typeFilter == 1)//Change by Category
        {
            foreach (Product pr in products)
                if (pr.Category == filter)
                    UpdatePrice(pr, average);
        }
        if (typeFilter == 2)//Change by Brand
        {
            foreach (Product pr in products)
                if (pr.Brand == filter)
                    UpdatePrice(pr, average);
        }
        if (typeFilter == 3)//Change by Name
        {
            foreach (Product pr in products)
                if (pr.Name == filter)
                    UpdatePrice(pr, average);
        }

        ReLoadContent();

        LoadFile();
    }
    private void UpdatePrice(Product pr, double avg)
    {
        pr.Price = pr.Price * avg;
        pr.Update = DateTime.Now.ToString("dd-MM-yyyy");
    }

    public void AddQuantToProduct(string name, string brand, int quant, double cost)//Used from purchases manager
    {
        foreach (Product pr in products)
            if ((pr.Name == name)&&(pr.Brand == brand))
            {
                pr.Quant += quant;
                pr.Cost = cost;//This's ok?

                ReLoadContent();
                LoadFile();
            }
    }

    public int GetProductQuant(string name, string brand)//Used from sales manager
    {
        foreach (Product pr in products)
            if ((pr.Name == name) && (pr.Brand == brand))
            {
                return pr.Quant;
            }
        return -1;
    }

    public bool SubQuantToProduct(string name, string brand, int quant)//Used from sales manager
    {
        foreach (Product pr in products)
            if ((pr.Name == name) && (pr.Brand == brand))
            {
                if (quant > pr.Quant)
                {
                    return false;
                }
                else
                {
                    pr.Quant -= quant;

                    ReLoadContent();
                    LoadFile();
                    return true;
                }
            }
        return false;
    }

    public void ReOrderContent(Comparison<Product> c)
    {
        products.Sort(c);
        ReLoadContent();
    }

    public void ReLoadContent()
    {
        Debug.Log("Reloading stock content");

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Product pr in products)
        {
            if(filter == null || filter.Satisfy(pr))
            {
                GameObject newP = (GameObject)Instantiate(productPrefab);
                newP.transform.SetParent(this.transform);
                LoadProduct(newP, pr.Code, pr.Name, pr.Brand, pr.Category, pr.Quant.ToString(), pr.Cost.ToString(), pr.Price.ToString(), pr.Update);
                newP.transform.localScale = new Vector3(1,1,1);
            }
        }
    }

    public void SetFilter(Filter f)
    {
        Debug.Log("Seting stock content filter");
        filter = f;
        ReLoadContent();
    }

    public List<string> GetProductsNames()//Used from purchase and sale products controllers (also from price panel)
    {
        List<string> r = new List<string>();
        foreach (Product pr in products)
            if(!r.Contains(pr.Name))
                r.Add(pr.Name);
        return r;
    }
    public List<string> GetProductsBrands()//Used from price panel controller
    {
        List<string> r = new List<string>();
        foreach (Product pr in products)
            if (!r.Contains(pr.Brand))
                r.Add(pr.Brand);
        return r;
    }
    public List<string> GetProductsCategorys()//Used from price panel controller
    {
        List<string> r = new List<string>();
        foreach (Product pr in products)
            if (!r.Contains(pr.Category))
                r.Add(pr.Category);
        return r;
    }

    public List<string> GetProductBrandsByName(string name)//Used from purchase and sale products controllers
    {
        List<string> r = new List<string>();
        foreach (Product pr in products)
            if(pr.Name.Equals(name))
                if(!r.Contains(pr.Brand))
                    r.Add(pr.Brand);
        return r;
    }

    public double GetProductPrice(string name, string brand)//Used from sale product controller
    {
        foreach (Product pr in products)
            if (pr.Name.Equals(name) && pr.Brand.Equals(brand))
                return pr.Price;
        return -1;//Error, product doesn't exist
    }

    public void DebugProducts()
    {
        string debug = "Lista de productos:";
        foreach (Product pr in products)
            debug += "\n" + pr.ToString();
        Debug.Log(debug);
    }

}