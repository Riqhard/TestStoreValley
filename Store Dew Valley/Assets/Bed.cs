using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bed : MonoBehaviour
{

    public SpriteRenderer icon;
    bool interactZoneActive = false;

    public delegate void BedDelegate();
    public BedDelegate bedEvent;

    public Transform positionToTeleportTo;

    public Animator infoAnimator;
    public Animator fadeAnimator;
    public TextMeshProUGUI tmpText;
    public int day = 0;

    int carrotSold;
    public TextMeshProUGUI carrotText;
    public TextMeshProUGUI dayText;
    public GameObject sleepWindow;
    public GameObject bedTimeStatsWindow;

    private bool menuOpen = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactZoneActive)
        {
            AudioManager.instance.PlayClip("ButtonPress");
            ToggleWindow();
        }
    }

    public void ToggleWindow()
    {
        sleepWindow.SetActive(!sleepWindow.activeSelf);
        

        if (menuOpen)
        {
            FindObjectOfType<UseItem>().menuOpen = false; 
        }
        else
        {
            FindObjectOfType<UseItem>().menuOpen = true;
        }

        menuOpen = !menuOpen;
    }

    public void GoToBed()
    {
        carrotSold = FindObjectOfType<ShopHandler>().carrotsSold;
        if (bedEvent != null)
        {
            bedEvent();
        }
        ToggleWindow();
        dayText.text = "Day " + day;
        day++;
        tmpText.text = "Day: " + day;

        fadeAnimator.SetTrigger("FadeBlack");
        AudioManager.instance.PlayClip("ButtonPress");
        StartCoroutine(WindowOpenUpTimer());
    }
    IEnumerator WindowOpenUpTimer()
    {
        yield return new WaitForSeconds(1f);
        bedTimeStatsWindow.SetActive(!bedTimeStatsWindow.activeSelf);
        carrotText.text = "";
    }

    public void StartDay()
    {
        bedTimeStatsWindow.SetActive(!bedTimeStatsWindow.activeSelf);
        StartCoroutine(StartDayWait());

        FindObjectOfType<PlayerMovement>().TeleportTo(positionToTeleportTo.position);
        AudioManager.instance.PlayClip("ButtonPress");
    }
    IEnumerator StartDayWait()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerMovement>().AlloweMovement();
        fadeAnimator.SetTrigger("FadeAway");
        infoAnimator.SetTrigger("ShowDay");
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = true;
            // Activate Icon
            icon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactZoneActive = false;
            // Activate Icon
            icon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            if (sleepWindow.activeSelf)
            {
                ToggleWindow();
            }
        }
    }
}
