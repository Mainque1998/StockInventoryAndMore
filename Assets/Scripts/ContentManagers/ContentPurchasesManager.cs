using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class ContentPurchasesManager : MonoBehaviour
{
    public GameObject purchasePrefab;

    public ContentManager stockManager;

    private List<Purchase> purchases = new List<Purchase>();

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/Compras.txt";
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

            purchases.Add(new Purchase(vars[0], vars[1], vars[2], vars[3], int.Parse(vars[4]), double.Parse(vars[5])));

            GameObject newP = (GameObject)Instantiate(purchasePrefab);
            newP.transform.SetParent(this.transform);
            LoadPurchase(newP, vars[0], vars[1], vars[2], vars[3], vars[4], vars[5]);

            p = sr.ReadLine();
        }
        sr.Close();
    }

    private void LoadFile()//Maybe it can be used from a button and not for each update
    {
        StreamWriter sw = new StreamWriter(filePath);

        foreach (Purchase p in purchases)
            sw.WriteLine(p.ToString());

        sw.Close();
    }

    public void AddNewPurchase(string date, string product, string brand, string supplier, string quant, string cost)
    {
        purchases.Add(new Purchase(date, product, brand, supplier, int.Parse(quant), double.Parse(cost)));
        Debug.Log("Se agreg� una compra nueva");

        GameObject newP = (GameObject)Instantiate(purchasePrefab);
        newP.transform.SetParent(this.transform);
        LoadPurchase(newP, date, product, brand, supplier, quant, cost);

        stockManager.AddQuantToProduct(product, brand, int.Parse(quant), double.Parse(cost));

        LoadFile();
    }

    private void LoadPurchase(GameObject p, string date, string product, string brand, string supplier, string quant, string cost)
    {
        TMP_Text[] vars = p.gameObject.GetComponentsInChildren<TMP_Text>();
        vars[0].text = date;
        vars[1].text = product;
        vars[2].text = brand;
        vars[3].text = supplier;
        vars[4].text = quant;
        vars[5].text = cost;
    }

    public void ReOrderContentByDate()
    {
        purchases.Sort(ComparePurchasesByDate);
        ReLoadContent();
    }
    private static int ComparePurchasesByDate(Purchase p1, Purchase p2)
    {
        string[] date = p1.Date.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Date.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d1.CompareTo(d2);
    }
    public void ReOrderContentByProductName()
    {
        purchases.Sort(ComparePurchasesByProductName);
        ReLoadContent();
    }
    private static int ComparePurchasesByProductName(Purchase p1, Purchase p2)
    {
        return p1.ProductName.CompareTo(p2.ProductName);
    }
    public void ReOrderContentByProductBrand()
    {
        purchases.Sort(ComparePurchasesByProductBrand);
        ReLoadContent();
    }
    private static int ComparePurchasesByProductBrand(Purchase p1, Purchase p2)
    {
        return p1.ProductBrand.CompareTo(p2.ProductBrand);
    }
    public void ReOrderContentBySupplier()
    {
        purchases.Sort(ComparePurchasesBySupplier);
        ReLoadContent();
    }
    private static int ComparePurchasesBySupplier(Purchase p1, Purchase p2)
    {
        return p1.Supplier.CompareTo(p2.Supplier);
    }
    public void ReOrderContentByQuant()
    {
        purchases.Sort(ComparePurchasesByQuant);
        ReLoadContent();
    }
    private static int ComparePurchasesByQuant(Purchase p1, Purchase p2)
    {
        return p1.Quant.CompareTo(p2.Quant);
    }
    public void ReOrderContentByCost()
    {
        purchases.Sort(ComparePurchasesByCost);
        ReLoadContent();
    }
    private static int ComparePurchasesByCost(Purchase p1, Purchase p2)
    {
        return p1.Cost.CompareTo(p2.Cost);
    }

    private void ReLoadContent()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Purchase pr in purchases)
        {
            GameObject newP = (GameObject)Instantiate(purchasePrefab);
            newP.transform.SetParent(this.transform);
            LoadPurchase(newP, pr.Date, pr.ProductName, pr.ProductBrand, pr.Supplier, pr.Quant.ToString(), pr.Cost.ToString());
        }
    }
}