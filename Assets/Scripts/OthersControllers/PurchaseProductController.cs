using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseProductController : MonoBehaviour
{
    private ContentManager stockManager;
    public TMP_Dropdown dropdownName;
    public TMP_Dropdown dropdownBrand;
    public TMP_InputField inputQ;
    public TMP_InputField inputC;
    private NewProductPanelController PanelController;

    public void SetSMandPC(ContentManager sM, NewProductPanelController pPC)
    {
        stockManager = sM;
        dropdownName.options.Add(new TMP_Dropdown.OptionData("Nuevo"));
        dropdownName.AddOptions(stockManager.GetProductsNames());
        PanelController = pPC;
    }

    public void ChangeProductName()
    {
        dropdownBrand.ClearOptions();
        if(dropdownName.value==1)
        {
            PanelController.OpenPanel(this.gameObject);
            dropdownName.value = 0;
        }
        else
        {
            string n = dropdownName.options[dropdownName.value].text;
            dropdownBrand.AddOptions(stockManager.GetProductBrandsByName(n));
        }
    }

    public void AddProduct(string name)
    {
        dropdownName.options.Add(new TMP_Dropdown.OptionData(name));
    }

    public void SetProduct(string name, string brand)
    {
        for (int i = 0; i < dropdownName.options.Count; i++)
            if (dropdownName.options[i].text.Equals(name))
                dropdownName.value = i;

        for (int i = 0; i < dropdownBrand.options.Count; i++)
            if (dropdownBrand.options[i].text.Equals(brand))
                dropdownBrand.value = i;
    }

    public bool ContainsName(string name)
    {
        foreach (TMP_Dropdown.OptionData o in dropdownName.options)
            if (o.text.Equals(name))
                return true;
        return false;
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
    public string GetCost()
    {
        return inputC.text;
    }
}
