using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PricePanelController : MonoBehaviour
{
    public GameObject panel;
    public TMP_Dropdown filters;
    public TMP_Dropdown filter;
    public TMP_InputField avg;
    public ContentManager stockManager;

    public void ChangePrices()
    {
        stockManager.UpdatePriceByFilters(filters.value, filter.options[filter.value].text, int.Parse(avg.text));//TODO: check filter and avg values

        ClosePanel();//Maybe we can leave it open
    }

    public void ClosePanel()
    {
        filter.ClearOptions();
        filter.gameObject.SetActive(false);

        avg.text = "0";

        filters.value = 0;

        panel.SetActive(false);
    }

    public void FilterChanged()
    {
        filter.ClearOptions();
        filter.gameObject.SetActive(filters.value != 0);

        if (filters.value == 1)
            filter.AddOptions(stockManager.GetProductsCategorys());
        else if (filters.value == 2)
            filter.AddOptions(stockManager.GetProductsBrands());
        else if (filters.value == 3)
            filter.AddOptions(stockManager.GetProductsNames());
    }
}
