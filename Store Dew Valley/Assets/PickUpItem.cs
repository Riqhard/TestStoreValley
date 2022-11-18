using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Seed"))
        {
            if (collision.GetComponent<Plant_placed>().cropIsReady)
            {
                collision.GetComponent<Plant_placed>().Pickup();
                AudioManager.instance.PlayClip("PlaceItem");
            }   
        }
    }
}
