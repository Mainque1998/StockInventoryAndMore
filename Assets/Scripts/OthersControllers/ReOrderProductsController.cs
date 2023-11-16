using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReOrderProductsController : MonoBehaviour
{
    public ContentManager stockManager;

    private bool codeFlag = false;
    private bool nameFlag = false;
    private bool brandFlag = false;
    private bool categoryFlag = false;
    private bool quantFlag = false;
    private bool costFlag = false;
    private bool priceFlag = false;
    private bool updateFlag = false;

    public void ReOrder(int i)
    {
        Comparison<Product> c=null;
        switch (i)
        {
            case 0:
                if (codeFlag)
                    c = CompareProductsByCodeA;
                else
                    c = CompareProductsByCodeD;
                codeFlag = !codeFlag;
                break;
            case 1:
                if (nameFlag)
                    c = CompareProductsByNameA;
                else
                    c = CompareProductsByNameD;
                nameFlag = !nameFlag;
                break;
            case 2:
                if (brandFlag)
                    c = CompareProductsByBrandA;
                else
                    c = CompareProductsByBrandD;
                brandFlag = !brandFlag;
                break;
            case 3:
                if (categoryFlag)
                    c = CompareProductsByCategoryA;
                else
                    c = CompareProductsByCategoryD;
                categoryFlag = !categoryFlag;
                break;
            case 4:
                if (quantFlag)
                    c = CompareProductsByQuantA;
                else
                    c = CompareProductsByQuantD;
                quantFlag = !quantFlag;
                break;
            case 5:
                if (costFlag)
                    c = CompareProductsByCostA;
                else
                    c = CompareProductsByCostD;
                costFlag = !costFlag;
                break;
            case 6:
                if (priceFlag)
                    c = CompareProductsByPriceA;
                else
                    c = CompareProductsByPriceD;
                priceFlag = !priceFlag;
                break;
            default:
                if (updateFlag)
                    c = CompareProductsByUpdateA;
                else
                    c = CompareProductsByUpdateD;
                updateFlag = !updateFlag;
                break;
        }
        stockManager.ReOrderContent(c);
    }

    private static int CompareProductsByCodeA(Product p1, Product p2)
    {
        return p1.Code.CompareTo(p2.Code);
    }
    private static int CompareProductsByCodeD(Product p1, Product p2)
    {
        return p2.Code.CompareTo(p1.Code);
    }

    private static int CompareProductsByNameA(Product p1, Product p2)
    {
        return p1.Name.CompareTo(p2.Name);
    }
    private static int CompareProductsByNameD(Product p1, Product p2)
    {
        return p2.Name.CompareTo(p1.Name);
    }

    private static int CompareProductsByBrandA(Product p1, Product p2)
    {
        return p1.Brand.CompareTo(p2.Brand);
    }
    private static int CompareProductsByBrandD(Product p1, Product p2)
    {
        return p2.Brand.CompareTo(p1.Brand);
    }

    private static int CompareProductsByCategoryA(Product p1, Product p2)
    {
        return p1.Category.CompareTo(p2.Category);
    }
    private static int CompareProductsByCategoryD(Product p1, Product p2)
    {
        return p2.Category.CompareTo(p1.Category);
    }

    private static int CompareProductsByQuantA(Product p1, Product p2)
    {
        return p1.Quant.CompareTo(p2.Quant);
    }
    private static int CompareProductsByQuantD(Product p1, Product p2)
    {
        return p2.Quant.CompareTo(p1.Quant);
    }

    private static int CompareProductsByCostA(Product p1, Product p2)
    {
        return p1.Cost.CompareTo(p2.Cost);
    }
    private static int CompareProductsByCostD(Product p1, Product p2)
    {
        return p2.Cost.CompareTo(p1.Cost);
    }

    private static int CompareProductsByPriceA(Product p1, Product p2)
    {
        return p1.Price.CompareTo(p2.Price);
    }
    private static int CompareProductsByPriceD(Product p1, Product p2)
    {
        return p2.Price.CompareTo(p1.Price);
    }

    private static int CompareProductsByUpdateA(Product p1, Product p2)
    {
        string[] date = p1.Update.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Update.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d1.CompareTo(d2);
    }
    private static int CompareProductsByUpdateD(Product p1, Product p2)
    {
        string[] date = p1.Update.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Update.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d2.CompareTo(d1);
    }

}
