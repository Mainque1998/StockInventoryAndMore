using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class PurchasePanelController : MonoBehaviour
{
    public ContentManager productsContentManager;
    public ProductPanelController productPanel;

    public GameObject purchasesContent;
    public TMP_InputField dayInput;//dd
    public TMP_InputField monthInput;//mm
    public TMP_InputField yearInput;//yyyy
    public TMP_Dropdown suppliersDropdown;
    public GameObject productsContent;
    public GameObject productContentPrefab;
    public GameObject NewSupplierPanel;

    private string filePath;
    private List<string> suppliers;

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
        //string actualDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
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
        newP.GetComponent<PurchaseProductController>().SetCMandPC(productsContentManager, productPanel);
    }

    public void ChangeSupplier()
    {
        if (suppliersDropdown.value == 1)
            NewSupplierPanel.SetActive(true);
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
            NewSupplierPanel.SetActive(false);//TODO: make the new supplier panel
        }
        else
        {
            Debug.Log("ERROR: ya existe el proveedor "+ns+".");
        }

    }

    public void Acept()
    {
        //TODO: instantiate all the new purchases on the content purchases
        ClosePanel();
    }
}
