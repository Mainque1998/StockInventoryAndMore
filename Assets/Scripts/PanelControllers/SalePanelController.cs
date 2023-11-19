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

    public NotificationPanelController notification;

    public void OpenPanel()
    {
        this.gameObject.SetActive(true);
        string[] actualDate = DateTime.Now.ToString("dd-MM-yyyy").Split('-');
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
        newP.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Acept()
    {
        string date = dayInput.text + "-" + monthInput.text + "-" + yearInput.text;//TODO: Check date.
        
        SaleProductController product;
        bool error = false;
        foreach (Transform child in productsContent.transform)
        {
            product = child.gameObject.GetComponent<SaleProductController>();
            if (product.GetName().Equals(" ") || product.GetBrand().Equals(" ") || product.GetQuant().Equals("0"))
            {
                Debug.Log("ERROR: Al menos un producto a vender esta mal definido");
                notification.OpenPanel("ERROR", "Uno o más productos de la venta están mal definidos. \nPor favor modifique los datos de los productos o cancele la venta.");
                return;
            }
        }

        foreach (Transform child in productsContent.transform)
        {
            product = child.gameObject.GetComponent<SaleProductController>();
            if (salesManager.AddNewSale(date, product.GetName(), product.GetBrand(), product.GetQuant(), product.GetPrice()))
                Destroy(child.gameObject);
            else
                error = true;
        }

        if(!error)
            ClosePanel();
    }
}
