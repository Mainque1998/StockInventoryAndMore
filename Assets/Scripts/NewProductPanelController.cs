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
    public ContentManager stockManager;
    public GameObject contentProductPurchase;

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
        categoryInput.text = "Categoría";
        priceInput.text = "0";
        this.gameObject.SetActive(false);
    }

    public void Accept()
    {
        bool error = false;
        error = !stockManager.AddNewProduct(codeInput.text, nameInput.text, brandInput.text, categoryInput.text, "0", "0", priceInput.text);
        if(!error)
        {
            //If the unit have the product name, all the others will too, else it will add the name on each one
            if (!productPurchase.GetComponent<PurchaseProductController>().ContainsName(nameInput.text))
            {
                foreach (Transform child in contentProductPurchase.transform)
                    child.gameObject.GetComponent<PurchaseProductController>().AddProduct(nameInput.text);
            }

            productPurchase.GetComponent<PurchaseProductController>().SetProduct(nameInput.text, brandInput.text);

            ClosePanel();
        }
    }
}
