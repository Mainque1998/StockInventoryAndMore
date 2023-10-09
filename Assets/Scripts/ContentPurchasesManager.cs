using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ContentPurchasesManager : MonoBehaviour
{
    public GameObject purchasePrefab;

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

            //purchases.Add(new Purchase(vars[0], vars[1], vars[2], vars[3], int.Parse(vars[4]), double.Parse(vars[5])));

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

    public void AddNewProduct(string date, string product, string brand, string supplier, string quant, string cost)
    {
        //purchases.Add(new Purchase(codigo, producto, marca, categoria, int.Parse(cant), double.Parse(costo), double.Parse(precio)));
        Debug.Log("Se agregó una compra nueva");

        GameObject newP = (GameObject)Instantiate(purchasePrefab);
        newP.transform.SetParent(this.transform);
        LoadPurchase(newP, date, product, brand, supplier, quant, cost);

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
}
