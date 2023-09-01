using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductPanelController : MonoBehaviour
{
    public GameObject panel;
    private bool newProduct;
    private GameObject product;

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

    public void LoadPanelFromProduct()//The only method used from product
    {
        ProductPanelController scriptPanel = GameObject.Find("AddButton").GetComponent<ProductPanelController>();
        panel = scriptPanel.panel;
        panel.SetActive(true);

        TMP_Text[] vars = this.gameObject.GetComponentsInChildren<TMP_Text>();

        //CAPAZ LOS INPUTFIELDS SE PUEDAN TENER COMO ATRIBUTOS DE LA CLASE, NO SE SI SE PIERDE LA REFERENCIA AL DESACTIVAR EL PANEL. TODO: PROBAR
        TMP_InputField inputField = GameObject.Find("CodigoInput").GetComponent<TMP_InputField>();
        inputField.text = vars[0].text;
        inputField = GameObject.Find("ProductoInput").GetComponent<TMP_InputField>();
        inputField.text = vars[1].text;
        inputField = GameObject.Find("MarcaInput").GetComponent<TMP_InputField>();
        inputField.text = vars[2].text;
        inputField = GameObject.Find("CategoriaInput").GetComponent<TMP_InputField>();
        inputField.text = vars[3].text;
        inputField = GameObject.Find("CantInput").GetComponent<TMP_InputField>();
        inputField.text = vars[4].text;
        inputField = GameObject.Find("CostoInput").GetComponent<TMP_InputField>();
        inputField.text = vars[5].text;
        inputField = GameObject.Find("PrecioInput").GetComponent<TMP_InputField>();
        inputField.text = vars[6].text;

        scriptPanel.SetProduct(this.gameObject);
    }

    public void ClosePanel()
    {
        TMP_InputField inputField = GameObject.Find("CodigoInput").GetComponent<TMP_InputField>();
        inputField.text = "00000000";
        inputField = GameObject.Find("ProductoInput").GetComponent<TMP_InputField>();
        inputField.text = "Producto";
        inputField = GameObject.Find("MarcaInput").GetComponent<TMP_InputField>();
        inputField.text = "Marca";
        inputField = GameObject.Find("CategoriaInput").GetComponent<TMP_InputField>();
        inputField.text = "Categoría";
        inputField = GameObject.Find("CantInput").GetComponent<TMP_InputField>();
        inputField.text = "0";
        inputField = GameObject.Find("CostoInput").GetComponent<TMP_InputField>();
        inputField.text = "0";
        inputField = GameObject.Find("PrecioInput").GetComponent<TMP_InputField>();
        inputField.text = "0";
        panel.SetActive(false);
    }

    public void Accept()
    {
        ContentManager contentScript = GameObject.Find("Content").GetComponent<ContentManager>();
        TMP_InputField codigo = GameObject.Find("CodigoInput").GetComponent<TMP_InputField>();
        TMP_InputField producto = GameObject.Find("ProductoInput").GetComponent<TMP_InputField>();
        TMP_InputField marca = GameObject.Find("MarcaInput").GetComponent<TMP_InputField>();
        TMP_InputField categoria = GameObject.Find("CategoriaInput").GetComponent<TMP_InputField>();
        TMP_InputField cant = GameObject.Find("CantInput").GetComponent<TMP_InputField>();
        TMP_InputField costo = GameObject.Find("CostoInput").GetComponent<TMP_InputField>();
        TMP_InputField precio = GameObject.Find("PrecioInput").GetComponent<TMP_InputField>();
        if (newProduct)
            contentScript.AddNewProduct(codigo.text, producto.text, marca.text, categoria.text, cant.text, costo.text, precio.text);
        else
            contentScript.UpdateProduct(product, codigo.text, producto.text, marca.text, categoria.text, cant.text, costo.text, precio.text);

        ClosePanel();
    }
}
