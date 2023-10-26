using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class PurchasePanelController : MonoBehaviour
{
    public ContentManager stockManager;
    public NewProductPanelController newProductPanel;

    public ContentPurchasesManager purchasesManager;

    public TMP_InputField dayInput;//dd
    public TMP_InputField monthInput;//mm
    public TMP_InputField yearInput;//yyyy

    public TMP_Dropdown suppliersDropdown;

    public GameObject productsContent;
    public GameObject productContentPrefab;

    public GameObject NewSupplierPanel;

    private string filePath;
    private List<string> suppliers = new List<string>();

    private void Start()
    {
        suppliersDropdown.options.Add(new TMP_Dropdown.OptionData("Nuevo"));
        filePath = Application.dataPath + "/Proveedores.txt";
        if (File.Exists(filePath))
            LoadSuppliers();
        else
            File.Create(filePath);
    }

    private void LoadSuppliers()
    {
        StreamReader sr = new StreamReader(filePath);
        string s = sr.ReadLine();
        while (s != null)
        {
            suppliers.Add(s);
            s = sr.ReadLine();
        }
        suppliersDropdown.AddOptions(suppliers);
        sr.Close();
    }
    private void LoadFile()
    {
        StreamWriter sw = new StreamWriter(filePath);
        foreach (string s in suppliers)
            sw.WriteLine(s);
        sw.Close();
    }

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
        newP.GetComponent<PurchaseProductController>().SetSMandPC(stockManager, newProductPanel);
    }

    public void ChangeSupplier()
    {
        if (suppliersDropdown.value == 1)
        {
            NewSupplierPanel.SetActive(true);
            suppliersDropdown.value = 0;
        }
    }
    public void NewSupplier()
    {
        string ns = NewSupplierPanel.GetComponentInChildren<TMP_InputField>().text;
        if(!suppliers.Contains(ns))
        {
            suppliers.Add(ns);
            LoadFile();
            suppliersDropdown.options.Add(new TMP_Dropdown.OptionData(ns));
            suppliersDropdown.value = suppliersDropdown.options.Count;
            CloseNewSupplierPanel();
        }
        else
        {
            Debug.Log("ERROR: ya existe el proveedor "+ns+".");
        }
    }
    public void CloseNewSupplierPanel()
    {
        NewSupplierPanel.GetComponentInChildren<TMP_InputField>().text = "";
        NewSupplierPanel.SetActive(false);
    }

    public void Acept()
    {
        string date = dayInput.text + "-" + monthInput.text + "-" + yearInput.text;//TODO: Check date.
        string supplier = suppliersDropdown.options[suppliersDropdown.value].text;
        if(supplier.Equals(" "))
        {
            Debug.Log("ERROR: Falta elegir proveedor");
            //TODO: return error to user
            return;
        }

        PurchaseProductController product;
        foreach (Transform child in productsContent.transform)
        {
            product = child.gameObject.GetComponent<PurchaseProductController>();
            if(!product.GetName().Equals(" ") && !product.GetBrand().Equals(" ") && !product.GetQuant().Equals("0")  && !product.GetCost().Equals("0"))
                purchasesManager.AddNewPurchase(date, product.GetName(), product.GetBrand(), supplier, product.GetQuant(), product.GetCost());
        }

        ClosePanel();
    }
}
