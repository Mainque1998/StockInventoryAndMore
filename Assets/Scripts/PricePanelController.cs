using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PricePanelController : MonoBehaviour
{
    public GameObject panel;
    public GameObject dropbox;
    public GameObject filterInput;

    public void ChangePrices()
    {
        TMP_InputField filter = filterInput.GetComponent<TMP_InputField>();
        TMP_InputField avg = GameObject.Find("AvgInput").GetComponent<TMP_InputField>();
        TMP_Dropdown filters = dropbox.GetComponent<TMP_Dropdown>();

        ContentManager contentScript = GameObject.Find("Content").GetComponent<ContentManager>();
        contentScript.UpdatePriceByFilters(filters.value, filter.text, int.Parse(avg.text));

        ClosePanel();//Capaz se pueda dejar abierto, para más modificaciones
    }

    public void ClosePanel()
    {
        TMP_InputField inputField = filterInput.GetComponent<TMP_InputField>();
        inputField.text = "";
        filterInput.SetActive(false);

        inputField = GameObject.Find("AvgInput").GetComponent<TMP_InputField>();
        inputField.text = "0";

        TMP_Dropdown filters = dropbox.GetComponent<TMP_Dropdown>();
        filters.value = 0;

        panel.SetActive(false);
    }

    public void FilterChanged()
    {
        TMP_Dropdown filters = dropbox.GetComponent<TMP_Dropdown>();
        filterInput.SetActive(filters.value != 0);
    }
}
