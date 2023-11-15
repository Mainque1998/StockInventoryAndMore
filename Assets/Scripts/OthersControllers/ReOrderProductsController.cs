using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReOrderProductsController : MonoBehaviour
{
    public ContentManager stockManager;

    public void ReOrder(int i)
    {
        switch (i)
        {
            case 0:
                stockManager.ReOrderContent(CompareProductsByCode);
                break;
            case 1:
                stockManager.ReOrderContent(CompareProductsByName);
                break;
            case 2:
                stockManager.ReOrderContent(CompareProductsByBrand);
                break;
            case 3:
                stockManager.ReOrderContent(CompareProductsByCategory);
                break;
            case 4:
                stockManager.ReOrderContent(CompareProductsByQuant);
                break;
            case 5:
                stockManager.ReOrderContent(CompareProductsByCost);
                break;
            case 6:
                stockManager.ReOrderContent(CompareProductsByPrice);
                break;
            default:
                stockManager.ReOrderContent(CompareProductsByUpdate);
                break;
        }
    }

    private static int CompareProductsByCode(Product p1, Product p2)
    {
        return p1.Code.CompareTo(p2.Code);
    }
    private static int CompareProductsByName(Product p1, Product p2)
    {
        return p1.Name.CompareTo(p2.Name);
    }
    private static int CompareProductsByBrand(Product p1, Product p2)
    {
        return p1.Brand.CompareTo(p2.Brand);
    }
    private static int CompareProductsByCategory(Product p1, Product p2)
    {
        return p1.Category.CompareTo(p2.Category);
    }
    private static int CompareProductsByQuant(Product p1, Product p2)
    {
        return p1.Quant.CompareTo(p2.Quant);
    }
    private static int CompareProductsByCost(Product p1, Product p2)
    {
        return p1.Cost.CompareTo(p2.Cost);
    }
    private static int CompareProductsByPrice(Product p1, Product p2)
    {
        return p2.Price.CompareTo(p1.Price);
    }
    private static int CompareProductsByUpdate(Product p1, Product p2)
    {
        string[] date = p1.Update.Split('-');
        DateTime d1 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        date = p2.Update.Split('-');
        DateTime d2 = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

        return d1.CompareTo(d2);
    }

}
