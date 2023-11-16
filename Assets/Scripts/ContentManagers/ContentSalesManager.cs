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
        if (stockManager.SubQuantToProduct(product, brand, int.Parse(quant)))
        {
            Debug.Log("Se agregó una venta nueva");

            GameObject newP = (GameObject)Instantiate(salePrefab);
            newP.transform.SetParent(this.transform);
            LoadSale(newP, date, product, brand, quant, price);

            sales.Add(new Sale(date, product, brand, int.Parse(quant), double.Parse(price)));

            LoadFile();

            return true;
        }
        else
        {
            Debug.Log("ERROR: Stock insuficiente para la venta del producto "+product+" de la marca "+brand);
            notification.OpenPanel("ERROR", "Stock insuficiente para la venta del producto " + product + " de la marca " + brand + ". \nPor favor modifique los datos o cancele la venta.");
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

    /*public void ReOrderContentByDate()
    {
        sales.Sort(CompareSalesByDate);
        ReLoadContent();
    }
    private static int CompareSalesByDate(Sale p1, Sale p2)
    {
        string[] date = p1.Date.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Date.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d1.CompareTo(d2);
    }
    public void ReOrderContentByProductName()
    {
        sales.Sort(CompareSalesByProductName);
        ReLoadContent();
    }
    private static int CompareSalesByProductName(Sale p1, Sale p2)
    {
        return p1.ProductName.CompareTo(p2.ProductName);
    }
    public void ReOrderContentByProductBrand()
    {
        sales.Sort(CompareSalesByProductBrand);
        ReLoadContent();
    }
    private static int CompareSalesByProductBrand(Sale p1, Sale p2)
    {
        return p1.ProductBrand.CompareTo(p2.ProductBrand);
    }
    public void ReOrderContentByQuant()
    {
        sales.Sort(CompareSalesByQuant);
        ReLoadContent();
    }
    private static int CompareSalesByQuant(Sale p1, Sale p2)
    {
        return p1.Quant.CompareTo(p2.Quant);
    }
    public void ReOrderContentByPrice()
    {
        sales.Sort(CompareSalesByPrice);
        ReLoadContent();
    }
    private static int CompareSalesByPrice(Sale p1, Sale p2)
    {
        return p1.Price.CompareTo(p2.Price);
    }*/

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
        }
    }
}
