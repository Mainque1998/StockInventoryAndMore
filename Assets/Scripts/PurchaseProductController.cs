using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseProductController : MonoBehaviour
{
    private ContentManager productsContentManager;
    public TMP_Dropdown dropdownName;
    public TMP_Dropdown dropdownBrand;
    private ProductPanelController productPanel;

    public void SetCMandPC(ContentManager pCM, ProductPanelController pPC)
    {
        productsContentManager = pCM;
        dropdownName.options.Add(new TMP_Dropdown.OptionData("Nuevo"));
        dropdownName.AddOptions(productsContentManager.GetProductsNames());
        productPanel = pPC;
    }

    public void ChangeProductName()
    {
        dropdownBrand.ClearOptions();
        if(dropdownName.value==1)
        {
            productPanel.OpenPanel();//TODO: it's must call SetNewProduct method
            dropdownName.value = 0;
        }
        else
        {
            string n = dropdownName.options[dropdownName.value].text;
            dropdownBrand.AddOptions(productsContentManager.GetProductBrandsByName(n));
        }
    }

    public void SetNewProduct(string name, string brand)
    {
        dropdownName.options.Add(new TMP_Dropdown.OptionData(name));
        dropdownName.value = dropdownName.options.Count;
        dropdownBrand.options.Add(new TMP_Dropdown.OptionData(brand));
        dropdownName.value = 0;
    }

    public void DeleteProduct()
    {
        Destroy(this.gameObject);
    }
}
