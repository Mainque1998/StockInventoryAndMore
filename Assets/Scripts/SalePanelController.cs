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
        bool actualError = false;
        foreach (Transform child in productsContent.transform)
        {
            product = child.gameObject.GetComponent<SaleProductController>();
            actualError = product.GetName().Equals(" ") || product.GetBrand().Equals(" ") || product.GetQuant().Equals("0");
            if (!actualError)
            {
                actualError = !salesManager.AddNewSale(date, product.GetName(), product.GetBrand(), product.GetQuant(), product.GetPrice());
                if (!actualError)
                    Destroy(child.gameObject);
                else
                    error = true;
            }
            else
            {
                Debug.Log("ERROR: Un producto a vender esta mal definido, modificar o eliminar");
                error = true;
            }
        }
        if(!error)
            ClosePanel();
        //So, the sales without errors are made, the sales with errors stay in the panel
    }
}
