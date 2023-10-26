using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sale : MonoBehaviour
{
    private string date;
    private string productName;
    private string productBrand;
    private int quant;
    private double price;

    public string Date { get => date; set => date = value; }
    public string ProductName { get => productName; set => productName = value; }
    public string ProductBrand { get => productBrand; set => productBrand = value; }
    public int Quant { get => quant; set => quant = value; }
    public double Price { get => price; set => price = value; }

    public Sale(string date, string productName, string productBrand, int quant, double price)
    {
        Date = date;
        ProductName = productName;
        ProductBrand = productBrand;
        Quant = quant;
        Price = price;
    }

    public void SetAll(string date, string productName, string productBrand, int quant, double price)
    {
        Date = date;
        ProductName = productName;
        ProductBrand = productBrand;
        Quant = quant;
        Price = price;
    }

    public override string ToString()
    {
        return Date + ";" + ProductName + ";" + ProductBrand + ";" + Quant + ";" + Price;
    }
}
