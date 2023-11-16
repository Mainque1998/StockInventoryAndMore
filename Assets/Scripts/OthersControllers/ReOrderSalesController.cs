using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReOrderSalesController : MonoBehaviour
{
    public ContentSalesManager contentManager;

    private bool dateFlag = true;
    private bool nameFlag = true;
    private bool brandFlag = true;
    private bool quantFlag = true;
    private bool priceFlag = true;

    public void ReOrder(int i)
    {
        Comparison<Sale> c = null;
        switch (i)
        {
            case 0:
                if (dateFlag)
                    c = CompareSalesByDateA;
                else
                    c = CompareSalesByDateD;
                dateFlag = !dateFlag;
                break;
            case 1:
                if (nameFlag)
                    c = CompareSalesByNameA;
                else
                    c = CompareSalesByNameD;
                nameFlag = !nameFlag;
                break;
            case 2:
                if (brandFlag)
                    c = CompareSalesByBrandA;
                else
                    c = CompareSalesByBrandD;
                brandFlag = !brandFlag;
                break;
            case 3:
                if (quantFlag)
                    c = CompareSalesByQuantA;
                else
                    c = CompareSalesByQuantD;
                quantFlag = !quantFlag;
                break;
            default:
                if (priceFlag)
                    c = CompareSalesByPriceA;
                else
                    c = CompareSalesByPriceD;
                priceFlag = !priceFlag;
                break;
        }
        contentManager.ReOrderContent(c);
    }

    private static int CompareSalesByDateA(Sale p1, Sale p2)
    {
        string[] date = p1.Date.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Date.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d1.CompareTo(d2);
    }
    private static int CompareSalesByDateD(Sale p1, Sale p2)
    {
        string[] date = p1.Date.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Date.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d2.CompareTo(d1);
    }

    private static int CompareSalesByNameA(Sale p1, Sale p2)
    {
        return p1.ProductName.CompareTo(p2.ProductName);
    }
    private static int CompareSalesByNameD(Sale p1, Sale p2)
    {
        return p2.ProductName.CompareTo(p1.ProductName);
    }

    private static int CompareSalesByBrandA(Sale p1, Sale p2)
    {
        return p1.ProductBrand.CompareTo(p2.ProductBrand);
    }
    private static int CompareSalesByBrandD(Sale p1, Sale p2)
    {
        return p2.ProductBrand.CompareTo(p1.ProductBrand);
    }

    private static int CompareSalesByQuantA(Sale p1, Sale p2)
    {
        return p1.Quant.CompareTo(p2.Quant);
    }
    private static int CompareSalesByQuantD(Sale p1, Sale p2)
    {
        return p2.Quant.CompareTo(p1.Quant);
    }

    private static int CompareSalesByPriceA(Sale p1, Sale p2)
    {
        return p1.Price.CompareTo(p2.Price);
    }
    private static int CompareSalesByPriceD(Sale p1, Sale p2)
    {
        return p2.Price.CompareTo(p1.Price);
    }
}
