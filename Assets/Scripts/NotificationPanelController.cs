using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationPanelController : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text message;

    public void OpenPanel(string t, string m)
    {
        title.text = t;
        message.text = m;
        this.gameObject.SetActive(true);
    }
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
