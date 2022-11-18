using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public int amount;

    private Inventory inventory;
    private Rigidbody2D rb;

    private TextMeshProUGUI amountText;
    private bool canBePickedUp;
    private bool weArePickingUpItem = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponentInChildren<SpriteRenderer>().sprite = item.icon;

        amountText = GetComponentInChildren<TextMeshProUGUI>();
        if (amount <= 1)
        {
            amountText.text = "";
        }
        else
        {
            amountText.text = "" + amount;
        }

        inventory = FindObjectOfType<Inventory>();

        canBePickedUp = false;
        StartCoroutine(timerForPickingUp());
        StartMovement();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && canBePickedUp)
        {
            TryPickingUpItem();
        }
    }

    public void TryPickingUpItem()
    {
        if (weArePickingUpItem)
        {
            return;
        }
        if (inventory.TryAddingItemToList(item.id, amount))
        {
            weArePickingUpItem = true;
            // We added item to player inventory.
            Vector2 dir = inventory.GetComponent<Transform>().position - transform.position;
 

            rb.AddForce(new Vector2 (dir.x * 400 / dir.magnitude , dir.y * 400 / dir.magnitude));

            StartCoroutine(DestroyTimer());
        }
        else
        {
            // Inventory full.
            NotificationUI.instance.ShowNotificationText("Inventory is full");
        }
    }

    public void StartMovement()
    {
        
        Vector2 force = new Vector2(Random.Range(-300, 300), Random.Range(0, 400));
        rb.AddForce(force);
        rb.gravityScale = 2f;
        rb.drag = 1f;

        StartCoroutine(ForceCancelTime());
        StartCoroutine(GravityCancelTime());
    }


    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    IEnumerator ForceCancelTime()
    {
        yield return new WaitForSeconds(0.1f);
        rb.drag = 5f;
    }
    IEnumerator GravityCancelTime()
    {
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = 0;
    }
    IEnumerator timerForPickingUp()
    {
        yield return new WaitForSeconds(1f);
        Vector2 dir = inventory.GetComponent<Transform>().position - transform.position;

        if (dir.magnitude <= 2)
        {
            TryPickingUpItem();
        }
        canBePickedUp = true;
    }
}
