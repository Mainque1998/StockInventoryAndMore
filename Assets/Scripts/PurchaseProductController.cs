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

    public void SetNewProduct(string name, string brand)
    {
        dropdownName.options.Add(new TMP_Dropdown.OptionData(name));
        dropdownName.value = dropdownName.options.Count -1;
        dropdownBrand.options.Add(new TMP_Dropdown.OptionData(brand));
        dropdownBrand.value = 0;
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
