using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    private string date;
    private string productName;
    private string productBrand;
    private string supplier;
    private int quant;
    private double cost;

    public string Date { get => date; set => date = value; }
    public string ProductName { get => productName; set => productName = value; }
    public string ProductBrand { get => productBrand; set => productBrand = value; }
    public string Supplier { get => supplier; set => supplier = value; }
    public int Quant { get => quant; set => quant = value; }
    public double Cost { get => cost; set => cost = value; }

    public Purchase(string date, string productName, string productBrand, string supplier, int quant, double cost)
    {
        Date = date;
        ProductName = productName;
        ProductBrand = productBrand;
        Supplier = supplier;
        Quant = quant;
        Cost = cost;
    }

    public void SetAll(string date, string productName, string productBrand, string supplier, int quant, double cost)
    {
        Date = date;
        ProductName = productName;
        ProductBrand = productBrand;
        Supplier = supplier;
        Quant = quant;
        Cost = cost;
    }

    public override string ToString()
    {
        return Date + ";" + ProductName + ";" + ProductBrand + ";" + Supplier + ";" + Quant + ";" + Cost;
    }
}
