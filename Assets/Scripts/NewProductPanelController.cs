using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewProductPanelController : MonoBehaviour
{
    private GameObject productPurchase;
    public TMP_InputField codeInput;
    public TMP_InputField nameInput;
    public TMP_InputField brandInput;
    public TMP_InputField categoryInput;
    public TMP_InputField priceInput;
    public ContentManager content;

    public void OpenPanel(GameObject pp)
    {
        productPurchase = pp;
        this.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        codeInput.text = "00000000";
        nameInput.text = "Producto";
        brandInput.text = "Marca";
        categoryInput.text = "Categor�a";
        priceInput.text = "0";
        this.gameObject.SetActive(false);
    }

    public void Accept()
    {
        content.AddNewProduct(codeInput.text, nameInput.text, brandInput.text, categoryInput.text, "0", "0", priceInput.text);
        productPurchase.GetComponent<PurchaseProductController>().SetNewProduct(nameInput.text, brandInput.text);

        ClosePanel();
    }
}
