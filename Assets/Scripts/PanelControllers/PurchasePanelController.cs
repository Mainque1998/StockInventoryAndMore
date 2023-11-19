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

    public NotificationPanelController notification;

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
        newP.GetComponent<PurchaseProductController>().SetSMandPC(stockManager, newProductPanel);
        newP.transform.localScale = new Vector3(1, 1, 1);
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
            notification.OpenPanel("ERROR", "Ya existe el proveedor " + ns + ". \nPor favor cambie el nombre o cancele la operación.");
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
            notification.OpenPanel("ERROR", "Falta elegir el proveedor de la compra. \nPor favor elija uno o cancele la operación.");
            return;
        }

        PurchaseProductController product;
        foreach (Transform child in productsContent.transform)
        {
            product = child.gameObject.GetComponent<PurchaseProductController>();
            if ( product.GetName().Equals(" ") || product.GetBrand().Equals(" ") || product.GetQuant().Equals("0") || product.GetCost().Equals("0") )
            {
                Debug.Log("ERROR: Al menos un producto a comprar esta mal definido");
                notification.OpenPanel("ERROR", "Uno o más productos de la compra están mal definidos. \nPor favor modifique los datos de los productos o cancele la compra.");
                return;
            }
        }

        foreach (Transform child in productsContent.transform)
        {
            product = child.gameObject.GetComponent<PurchaseProductController>();
            purchasesManager.AddNewPurchase(date, product.GetName(), product.GetBrand(), supplier, product.GetQuant(), product.GetCost());
        }
        
        ClosePanel();
    }
}
