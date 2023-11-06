using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ContentManager : MonoBehaviour
{
    public GameObject productPrefab;

    private List<Product> products = new List<Product>();

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/Stock.txt";
        if (File.Exists(filePath))
            LoadContent();
        else
            File.Create(filePath);
    }

    private void LoadContent()
    {
        Debug.Log("Cargando stock.");
        StreamReader sr = new StreamReader(filePath);
        string p = sr.ReadLine();
        while (p!= null)
        {
            string[] vars = p.Split(';');

            products.Add(new Product(vars[0], vars[1], vars[2], vars[3], int.Parse(vars[4]), double.Parse(vars[5]), double.Parse(vars[6])));
            
            GameObject newP = (GameObject)Instantiate(productPrefab);
            newP.transform.SetParent(this.transform);
            LoadProduct(newP, vars[0], vars[1], vars[2], vars[3], vars[4], vars[5], vars[6]);

            p = sr.ReadLine();
        }
        sr.Close();
    }

    private void LoadFile()//Maybe it can be used from a button and not for each update
    {
        StreamWriter sw = new StreamWriter(filePath);

        foreach (Product pr in products)
            sw.WriteLine( pr.ToString() );

        sw.Close();
    }

    public bool AddNewProduct(string code, string name, string brand, string category, string quant, string cost, string price)
    {
        Product p = new Product(code, name, brand, category, int.Parse(quant), double.Parse(cost), double.Parse(price));
        if (products.Contains(p))
        {
            Debug.Log("ERROR: Ya existe el producto: " + code + ", nombre "+ name + " de la marca "+brand);
            //TODO: return error to user
            return false;
        }
        products.Add(p);
        Debug.Log("Se agregó el producto: " + code + ", nombre " + name + " de la marca " + brand);

        GameObject newP = (GameObject)Instantiate(productPrefab);
        newP.transform.SetParent(this.transform);
        LoadProduct(newP, code, name, brand, category, quant, cost, price);

        LoadFile();

        return true;
    }

    public bool UpdateProduct(GameObject p, string code, string name, string brand, string category, string quant, string cost, string price)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        if ( (vars[0].text != code || vars[1].text != name || vars[2].text != brand) //It mean, they're the same keys
            && products.Contains(new Product(code, name, brand)))
        {
            Debug.Log("ERROR: Ya existe el producto: " + code + ", nombre " + name + " de la marca " + brand);
            //TODO: return error to user
            return false;
        }

        int posP = products.IndexOf(new Product(vars[0].text));
        products[posP].SetAll(code, name, brand, category, int.Parse(quant), double.Parse(cost), double.Parse(price));
        Debug.Log("Se modificó el producto con código " + vars[0].text);

        LoadProduct(p, code, name, brand, category, quant, cost, price);

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

    private void LoadProduct(GameObject p, string code, string product, string brand, string category, string quant, string cost, string price)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        vars[0].text = code;
        vars[1].text = product;
        vars[2].text = brand;
        vars[3].text = category;
        vars[4].text = quant;
        vars[5].text = cost;
        vars[6].text = price;
    }

    public void UpdatePriceByFilters(int typeFilter, string filter, int avg)
    {
        avg += 100;
        double average = double.Parse(avg.ToString()) / 100;
        if (typeFilter == 0)//Change all
        {
            foreach (Product pr in products)
                pr.Price = pr.Price * average;
        }
        if (typeFilter == 1)//Change by Category
        {
            foreach (Product pr in products)
                if (pr.Category == filter)
                {
                    pr.Price = pr.Price * average;
                }
        }
        if (typeFilter == 2)//Change by Brand
        {
            foreach (Product pr in products)
                if (pr.Brand == filter)
                {
                    pr.Price = pr.Price * average;
                }
        }

        ReLoadContent();

        LoadFile();
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

    public void ReOrderContentByCode()
    {
        products.Sort(CompareProductsByCode);
        ReLoadContent();

    }
    private static int CompareProductsByCode(Product p1, Product p2)
    {
        return p1.Code.CompareTo(p2.Code);
    }
    public void ReOrderContentByName()
    {
        products.Sort(CompareProductsByName);
        ReLoadContent();
    }
    private static int CompareProductsByName(Product p1, Product p2)
    {
        return p1.Name.CompareTo(p2.Name);
    }
    public void ReOrderContentByBrand()
    {
        products.Sort(CompareProductsByBrand);
        ReLoadContent();
    }
    private static int CompareProductsByBrand(Product p1, Product p2)
    {
        return p1.Brand.CompareTo(p2.Brand);
    }
    public void ReOrderContentByCategory()
    {
        products.Sort(CompareProductsByCategory);
        ReLoadContent();
    }
    private static int CompareProductsByCategory(Product p1, Product p2)
    {
        return p1.Category.CompareTo(p2.Category);
    }
    public void ReOrderContentByQuant()
    {
        products.Sort(CompareProductsByQuant);
        ReLoadContent();
    }
    private static int CompareProductsByQuant(Product p1, Product p2)
    {
        return p1.Quant.CompareTo(p2.Quant);
    }
    public void ReOrderContentByCost()
    {
        products.Sort(CompareProductsByCost);
        ReLoadContent();
    }
    private static int CompareProductsByCost(Product p1, Product p2)
    {
        return p1.Cost.CompareTo(p2.Cost);
    }
    public void ReOrderContentByPrice()
    {
        products.Sort(CompareProductsByPrice);
        ReLoadContent();
    }
    private static int CompareProductsByPrice(Product p1, Product p2)
    {
        return p1.Price.CompareTo(p2.Price);
    }

    private void ReLoadContent()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Product pr in products)
        {
            GameObject newP = (GameObject)Instantiate(productPrefab);
            newP.transform.SetParent(this.transform);
            LoadProduct(newP, pr.Code, pr.Name, pr.Brand, pr.Category, pr.Quant.ToString(), pr.Cost.ToString(), pr.Price.ToString());
        }
    }

    public List<string> GetProductsNames()//Used from purchase and sale products controllers
    {
        List<string> r = new List<string>();
        foreach (Product pr in products)
            if(!r.Contains(pr.Name))
                r.Add(pr.Name);
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