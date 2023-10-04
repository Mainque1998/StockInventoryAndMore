using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsController : MonoBehaviour
{
    public GameObject productsPanel;
    public GameObject purchasesPanel;
    public GameObject salesPanel;
    public GameObject accountPanel;
    private GameObject actualPanel = null;

    private void CloseOldPanel()
    {
        if (actualPanel != null)
            actualPanel.SetActive(false);
    }
    public void OpenProducts()
    {
        CloseOldPanel();
        actualPanel = productsPanel;
        actualPanel.SetActive(true);
    }
    public void OpenPurchases()
    {
        CloseOldPanel();
        actualPanel = purchasesPanel;
        actualPanel.SetActive(true);
    }
    public void OpenSales()
    {
        CloseOldPanel();
        actualPanel = salesPanel;
        actualPanel.SetActive(true);
    }
    public void OpenAccount()
    {
        CloseOldPanel();
        actualPanel = accountPanel;
        actualPanel.SetActive(true);
    }
}
