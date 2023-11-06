using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductPanelController : MonoBehaviour
{
    private bool newProduct;
    private GameObject product;
    public GameObject panel;
    public TMP_InputField codeInput;
    public TMP_InputField nameInput;
    public TMP_InputField brandInput;
    public TMP_InputField categoryInput;
    public TMP_InputField quantInput;
    public TMP_InputField costInput;
    public TMP_InputField priceInput;
    public GameObject content;

    public void OpenPanel()
    {
        newProduct = true;
        panel.SetActive(true);
    }

    public void SetProduct(GameObject p)
    {
        product = p;
        newProduct = false;
    }

    public void ClosePanel()
    {
        LoadPanel("00000000", "Producto", "Marca", "Categoría", "0", "0", "0");
        panel.SetActive(false);
    }

    public void LoadPanelFromProduct()//The only method used from product
    {
        ProductPanelController scriptPanel = GameObject.FindWithTag("AddButton").GetComponent<ProductPanelController>();
        panel = scriptPanel.panel;
        panel.SetActive(true);

        TMP_Text[] vars = this.gameObject.GetComponentsInChildren<TMP_Text>();

        scriptPanel.LoadPanel(vars[0].text, vars[1].text, vars[2].text, vars[3].text, vars[4].text, vars[5].text, vars[6].text);

        scriptPanel.SetProduct(this.gameObject);
    }

    private void LoadPanel(string code, string name, string brand, string category, string quant, string cost, string price)
    {
        codeInput.text = code;
        nameInput.text = name;
        brandInput.text = brand;
        categoryInput.text = category;
        quantInput.text = quant;
        costInput.text = cost;
        priceInput.text = price;
    }

    public void Accept()
    {
        ContentManager contentScript = content.GetComponent<ContentManager>();
        bool error = false;
        if (newProduct)
            error = !contentScript.AddNewProduct(codeInput.text, nameInput.text, brandInput.text, categoryInput.text, quantInput.text, costInput.text, priceInput.text);
        else
            error = !contentScript.UpdateProduct(product, codeInput.text, nameInput.text, brandInput.text, categoryInput.text, quantInput.text, costInput.text, priceInput.text);

        if(!error)
            ClosePanel();
    }

    public void Delete()
    {
        if (!newProduct)
        {
            ContentManager contentScript = content.GetComponent<ContentManager>();
            contentScript.DeleteProduct(product);
        }

        ClosePanel();
    }
}
