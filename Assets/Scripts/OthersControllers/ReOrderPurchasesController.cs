using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReOrderPurchasesController : MonoBehaviour
{
    public ContentPurchasesManager contentManager;

    private bool dateFlag = true;
    private bool nameFlag = true;
    private bool brandFlag = true;
    private bool supplierFlag = true;
    private bool quantFlag = true;
    private bool costFlag = true;

    public void ReOrder(int i)
    {
        Comparison<Purchase> c = null;
        switch (i)
        {
            case 0:
                if (dateFlag)
                    c = ComparePurchasesByDateA;
                else
                    c = ComparePurchasesByDateD;
                dateFlag = !dateFlag;
                break;
            case 1:
                if (nameFlag)
                    c = ComparePurchasesByNameA;
                else
                    c = ComparePurchasesByNameD;
                nameFlag = !nameFlag;
                break;
            case 2:
                if (brandFlag)
                    c = ComparePurchasesByBrandA;
                else
                    c = ComparePurchasesByBrandD;
                brandFlag = !brandFlag;
                break;
            case 3:
                if (supplierFlag)
                    c = ComparePurchasesBySupplierA;
                else
                    c = ComparePurchasesBySupplierD;
                supplierFlag = !supplierFlag;
                break;
            case 4:
                if (quantFlag)
                    c = ComparePurchasesByQuantA;
                else
                    c = ComparePurchasesByQuantD;
                quantFlag = !quantFlag;
                break;
            default:
                if (costFlag)
                    c = ComparePurchasesByCostA;
                else
                    c = ComparePurchasesByCostD;
                costFlag = !costFlag;
                break;
        }
        contentManager.ReOrderContent(c);
    }

    private static int ComparePurchasesByDateA(Purchase p1, Purchase p2)
    {
        string[] date = p1.Date.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Date.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d1.CompareTo(d2);
    }
    private static int ComparePurchasesByDateD(Purchase p1, Purchase p2)
    {
        string[] date = p1.Date.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Date.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d2.CompareTo(d1);
    }

    private static int ComparePurchasesByNameA(Purchase p1, Purchase p2)
    {
        return p1.ProductName.CompareTo(p2.ProductName);
    }
    private static int ComparePurchasesByNameD(Purchase p1, Purchase p2)
    {
        return p2.ProductName.CompareTo(p1.ProductName);
    }

    private static int ComparePurchasesByBrandA(Purchase p1, Purchase p2)
    {
        return p1.ProductBrand.CompareTo(p2.ProductBrand);
    }
    private static int ComparePurchasesByBrandD(Purchase p1, Purchase p2)
    {
        return p2.ProductBrand.CompareTo(p1.ProductBrand);
    }

    private static int ComparePurchasesBySupplierA(Purchase p1, Purchase p2)
    {
        return p1.Supplier.CompareTo(p2.Supplier);
    }
    private static int ComparePurchasesBySupplierD(Purchase p1, Purchase p2)
    {
        return p2.Supplier.CompareTo(p1.Supplier);
    }

    private static int ComparePurchasesByQuantA(Purchase p1, Purchase p2)
    {
        return p1.Quant.CompareTo(p2.Quant);
    }
    private static int ComparePurchasesByQuantD(Purchase p1, Purchase p2)
    {
        return p2.Quant.CompareTo(p1.Quant);
    }

    private static int ComparePurchasesByCostA(Purchase p1, Purchase p2)
    {
        return p1.Cost.CompareTo(p2.Cost);
    }
    private static int ComparePurchasesByCostD(Purchase p1, Purchase p2)
    {
        return p2.Cost.CompareTo(p1.Cost);
    }
}
