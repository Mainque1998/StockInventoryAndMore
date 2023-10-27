using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class SalePanelController : MonoBehaviour
{
    public ContentManager stockManager;

    public ContentSalesManager salesManager;

    public TMP_InputField dayInput;//dd
    public TMP_InputField monthInput;//mm
    public TMP_InputField yearInput;//yyyy

    public GameObject productsContent;
    public GameObject productContentPrefab;

    public void OpenPanel()
    {
        this.gameObject.SetActive(true);
        string[] actualDate = DateTime.UtcNow.ToString("dd-MM-yyyy").Split('-');
        dayInput.text = actualDate[0];
        monthInput.text = actualDate[1];
        yearInput.text = actualDate[2];
        AddNewProduct();
    }

    public void ClosePanel()
    {
        foreach (Transform child in productsContent.transform)
        {
            Destroy(child.gameObject);
        }
        this.gameObject.SetActive(false);
    }

    public void AddNewProduct()
    {
        GameObject newP = (GameObject)Instantiate(productContentPrefab);
        newP.transform.SetParent(productsContent.transform);
        newP.GetComponent<SaleProductController>().SetStock(stockManager);
    }

    public void Acept()
    {
        string date = dayInput.text + "-" + monthInput.text + "-" + yearInput.text;//TODO: Check date.
        
        SaleProductController product;
        bool error = false;
        foreach (Transform child in productsContent.transform)
        {
            product = child.gameObject.GetComponent<SaleProductController>();
            //TODO: Check if the add is correctly done
            if (!product.GetName().Equals(" ") && !product.GetBrand().Equals(" ") && !product.GetQuant().Equals("0"))
            {
                error = !salesManager.AddNewSale(date, product.GetName(), product.GetBrand(), product.GetQuant(), product.GetPrice());
                if(!error)
                    Destroy(child.gameObject);
            }
        }

        if(!error)
            ClosePanel();
        //So, the sales that doesn't have errors are made, the sales with errors stay in the panel
    }
}
