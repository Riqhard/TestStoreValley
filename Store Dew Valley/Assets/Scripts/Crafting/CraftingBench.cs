using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingBench : MonoBehaviour
{
    public GameObject indicator;
    public GameObject progressBar;
    public Image fillBar;
    public float buildTime;

    public bool buildDone = false;

    public bool interactionOn;

    private bool interactZoneActive = false;

    private void Start()
    {
        interactZoneActive = false;
        indicator.SetActive(false);

        fillBar.fillAmount = 0;
        progressBar.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            interactionOn = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && interactZoneActive && !buildDone)
        {
            interactionOn = true;
        }



        if (interactionOn)
        {
            if (!progressBar.activeSelf)
            {
                progressBar.SetActive(true);
            }
            fillBar.fillAmount += 1.0f / buildTime * Time.deltaTime;
            if (fillBar.fillAmount >= 1)
            {
                BuildDone();
            }
        }
        else
        {
            fillBar.fillAmount -= 1.0f / buildTime * Time.deltaTime;
            if (fillBar.fillAmount <= 0)
            {
                progressBar.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !interactZoneActive && !buildDone)
        {
            // Interact Zone active
            interactZoneActive = true;
            indicator.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactZoneActive)
        {
            // Interact Zone Off
            interactZoneActive = false;
            indicator.SetActive(false);

            interactionOn = false;

            fillBar.fillAmount = 0;
            progressBar.SetActive(false);
        }
    }
    public void BuildDone()
    {
        indicator.SetActive(false);
        progressBar.SetActive(false);
        interactionOn = false;
        buildDone = true;

        Debug.Log("Build DONE!");
    }

}
