using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelsController : MonoBehaviour
{
    public GameObject productsPanel;
    public GameObject purchasesPanel;
    public GameObject salesPanel;
    public GameObject accountPanel;
    private GameObject actualPanel;
    public GameObject productsButton;
    public GameObject purchasesButton;
    public GameObject salesButton;
    public GameObject accountButton;
    private GameObject actualButton;
    public Color selectedColor;

    private void Start()
    {
        actualPanel = productsPanel;
        actualButton = productsButton;
        SelectButton();
    }

    private void CloseOldPanel()
    {
        if (actualPanel != null)
        {
            actualPanel.SetActive(false);
            DeselectButton();
        }
    }
    private void DeselectButton()
    {
        actualButton.GetComponent<Image>().color = Color.white;
        actualButton.GetComponentInChildren<TMP_Text>().color = Color.black;
    }

    private void OpenNewPanel(GameObject p, GameObject b)
    {
        CloseOldPanel();
        actualPanel = p;
        actualPanel.SetActive(true);
        actualButton = b;
        SelectButton();
    }
    private void SelectButton()
    {
        actualButton.GetComponent<Image>().color = selectedColor;
        actualButton.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

    public void OpenProducts()
    {
        OpenNewPanel(productsPanel, productsButton);
    }
    public void OpenPurchases()
    {
        OpenNewPanel(purchasesPanel, purchasesButton);
    }
    public void OpenSales()
    {
        OpenNewPanel(salesPanel, salesButton);
    }
    public void OpenAccount()
    {
        OpenNewPanel(accountPanel, accountButton);
    }
}
