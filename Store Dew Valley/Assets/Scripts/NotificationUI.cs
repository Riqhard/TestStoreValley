using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationUI : MonoBehaviour
{
    public TextMeshProUGUI proUGUI;

    public static NotificationUI instance;
    bool showingText = false;

    private void Start()
    {
        instance = this;
    }

    public void ShowNotificationText(string notificationText)
    {
        if (!showingText)
        {
            proUGUI.text = notificationText;
            GetComponent<Animator>().SetTrigger("Show");
            showingText = true;
            StopAllCoroutines();
            StartCoroutine(textTimer());
        }
    }
    IEnumerator textTimer()
    {
        yield return new WaitForSeconds(3f);

        showingText = false;
    }
}
