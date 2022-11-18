using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    public Obsticle obs;

    public GameObject indicator;
    public GameObject progressBar;
    public Image fillBar;
    public GameObject droppedItemPrefab;

    private ItemDatabase itemDatabase;

    private bool interactionOn;
    private bool interactZoneActive = false;

    private void Start()
    {
        interactZoneActive = false;
        indicator.SetActive(false);
        fillBar.fillAmount = 0;
        progressBar.SetActive(false);

        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            interactionOn = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && interactZoneActive)
        {
            interactionOn = true;
        }
        if (interactionOn)
        {
            if (!progressBar.activeSelf)
            {
                progressBar.SetActive(true);
            }
            fillBar.fillAmount += 1.0f / obs.buildTime * Time.deltaTime;
            if (fillBar.fillAmount >= 1)
            {
                BuildDone();
            }
        }
    }
    public void BuildDone()
    {
        indicator.SetActive(false);
        progressBar.SetActive(false);
        interactionOn = false;

        // Pop out items

        if (obs.dropItem)
        {
            int i = 0;
            foreach (int itemId in obs.itemsToDrop)
            {
                
                Item item = itemDatabase.GetItem(itemId);

                if (item != null)
                {
                    DropItem(item, obs.itemAmountsToDrop[i]);
                }

                i++;
            }
        }


        if (obs.transformObstacle)
        {
            Instantiate(obs.transformPrefab, transform.position, Quaternion.identity);
        }
        

        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !interactZoneActive)
        {
            interactZoneActive = true;
            indicator.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactZoneActive)
        {
            interactZoneActive = false;
            indicator.SetActive(false);
            interactionOn = false;
        }
    }



    public void DropItem(Item item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity);
            instance.GetComponent<ItemPickup>().item = item;
            instance.GetComponent<ItemPickup>().amount = 1;
        }
    }
}
