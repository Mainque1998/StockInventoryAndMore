using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class ContentSalesManager : MonoBehaviour
{
    public GameObject salePrefab;

    public ContentManager stockManager;

    private List<Sale> sales = new List<Sale>();

    private string filePath;

    public NotificationPanelController notification;

    private void Start()
    {
        filePath = Application.dataPath + "/Ventas.txt";
        if (File.Exists(filePath))
            LoadContent();
        else
            File.Create(filePath);
    }

    private void LoadContent()
    {
        StreamReader sr = new StreamReader(filePath);
        string p = sr.ReadLine();
        while (p != null)
        {
            string[] vars = p.Split(';');

            sales.Add(new Sale(vars[0], vars[1], vars[2], int.Parse(vars[3]), double.Parse(vars[4])));

            GameObject newP = (GameObject)Instantiate(salePrefab);
            newP.transform.SetParent(this.transform);
            LoadSale(newP, vars[0], vars[1], vars[2], vars[3], vars[4]);
            newP.transform.localScale = new Vector3(1, 1, 1);

            p = sr.ReadLine();
        }
        sr.Close();
    }

    private void LoadFile()//Maybe it can be used from a button and not for each update
    {
        StreamWriter sw = new StreamWriter(filePath);

        foreach (Sale s in sales)
            sw.WriteLine(s.ToString());

        sw.Close();
    }

    public bool AddNewSale(string date, string product, string brand, string quant, string price)
    {
        //TODO: maybe we can first check the stock, and in case of insuficience notify the user
        int stock = stockManager.GetProductQuant(product, brand);

        if(stock == -1)
        {
            Debug.Log("ERROR: No se encuentra el producto " + product + " de la marca " + brand);
            notification.OpenPanel("ERROR", "No se encuentra el producto " + product + " de la marca " + brand + ". \nPor favor modifique los datos o cancele la venta.");
            return false;
        }

        if (stock >= int.Parse(quant))
        {
            stockManager.SubQuantToProduct(product, brand, int.Parse(quant));
            Debug.Log("Se agreg� una venta nueva");

            GameObject newP = (GameObject)Instantiate(salePrefab);
            newP.transform.SetParent(this.transform);
            LoadSale(newP, date, product, brand, quant, price);
            newP.transform.localScale = new Vector3(1, 1, 1);

            sales.Add(new Sale(date, product, brand, int.Parse(quant), double.Parse(price)));

            LoadFile();

            return true;
        }
        else
        {
            Debug.Log("ERROR: Stock insuficiente para la venta del producto "+product+" de la marca "+brand);
            notification.OpenPanel("ERROR", "Stock insuficiente ("+stock+") para la venta del producto " + product + " de la marca " + brand + ". \nPor favor modifique los datos o cancele la venta.");
            return false;
        }
    }

    private void LoadSale(GameObject p, string date, string product, string brand, string quant, string price)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        vars[0].text = date;
        vars[1].text = product;
        vars[2].text = brand;
        vars[3].text = quant;
        vars[4].text = price;
    }
    public void ReOrderContent(Comparison<Sale> c)
    {
        sales.Sort(c);
        ReLoadContent();
    }

    private void ReLoadContent()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Sale s in sales)
        {
            GameObject newP = (GameObject)Instantiate(salePrefab);
            newP.transform.SetParent(this.transform);
            LoadSale(newP, s.Date, s.ProductName, s.ProductBrand, s.Quant.ToString(), s.Price.ToString());
            newP.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
