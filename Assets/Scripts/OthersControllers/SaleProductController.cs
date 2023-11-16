using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaleProductController : MonoBehaviour
{
    private ContentManager stockManager;
    public TMP_Dropdown dropdownName;
    public TMP_Dropdown dropdownBrand;
    public TMP_InputField inputQ;
    public TMP_InputField inputP;

    public void SetStock(ContentManager stockM)
    {
        stockManager = stockM;
        dropdownName.AddOptions(stockManager.GetProductsNames());
    }

    public void ChangeProductName()
    {
        dropdownBrand.ClearOptions();
        string n = dropdownName.options[dropdownName.value].text;
        dropdownBrand.AddOptions(stockManager.GetProductBrandsByName(n));
        ChangeProductBrand();
    }

    public void ChangeProductBrand()
    {
        string n = dropdownName.options[dropdownName.value].text;
        string b = dropdownBrand.options[dropdownBrand.value].text;
        inputP.text = stockManager.GetProductPrice(n, b).ToString();
    }

    public void DeleteProduct()
    {
        Destroy(this.gameObject);
    }

    public string GetName()
    {
        return dropdownName.options[dropdownName.value].text;
    }
    public string GetBrand()
    {
        return dropdownBrand.options[dropdownBrand.value].text;
    }
    public string GetQuant()
    {
        return inputQ.text;
    }
    public string GetPrice()
    {
        return inputP.text;
    }
}
