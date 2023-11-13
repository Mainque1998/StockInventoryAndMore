using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class LatestUpdatesController : MonoBehaviour
{
    public ContentManager stockManager;
    public GameObject LatestUpdatesPanel;
    public TMP_InputField inputD;
    public TMP_InputField inputM;
    public TMP_InputField inputY;

    public void Start()
    {
        string ud = "<mark =#ffff00>"; //TODO: Check if is better highlighted or no
        string filePath = Application.dataPath + "/Stock.txt";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            ud += sr.ReadLine();
        }
        else
        {
            ud += DateTime.Now.ToString("dd-MM-yyyy");
        }
        this.GetComponentInChildren<TMP_Text>().text = ud;
    }

    public void OpenPanel()
    {
        LatestUpdatesPanel.gameObject.SetActive(true);
        string ud = this.GetComponentInChildren<TMP_Text>().text.Substring(15);
        string[] uDate = ud.Split('-');
        inputD.text = uDate[0];
        inputM.text = uDate[1];
        inputY.text = uDate[2];
    }

    public void Accept()
    {
        string ud = inputD.text +"-"+ inputM.text +"-"+ inputY.text;//TODO: Check date.
        this.GetComponentInChildren<TMP_Text>().text = "<mark =#ffff00>" + ud;

        stockManager.SetUpdatesDate(new DateTime(int.Parse(inputY.text), int.Parse(inputM.text), int.Parse(inputD.text)));

        LatestUpdatesPanel.gameObject.SetActive(false);
    }
}
