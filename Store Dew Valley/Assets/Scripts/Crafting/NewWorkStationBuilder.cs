using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorkStationBuilder : MonoBehaviour
{

    public GameObject workStationBuilderWindow;
    private Transform player;

    bool interactZoneActive = false;

    public Transform icon;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactZoneActive)
        {
            ToggleWindowOpen();
        }
    }

    public void ToggleWindowOpen()
    {
        workStationBuilderWindow.SetActive(!workStationBuilderWindow.activeSelf);
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
            workStationBuilderWindow.SetActive(false);
            // Hide Icon
            icon.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }
}
