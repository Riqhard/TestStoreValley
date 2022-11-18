using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public SlotPanel inventorySlotPanel;
    public ToolbarIndicator toolbarIndicator;

    public CropPlacementChecker cropChecker;

    public LayerMask targetMask;

    public float cooldownTime;
    private float cooldownReady = 0;

    PlayerMovement playerMovement;
    public bool menuOpen = false;

    public Animator animator;

    public Transform point;

    public GameObject pickupGameobject;

    public Direction direction = Direction.Right;

    PlayerUIController uIController;
    public float toolPositionOffset;

    private void Start()
    {
        // Subscribe to delegate
        uIController = FindObjectOfType<PlayerUIController>();
        playerMovement = PlayerMovement.instance;
    }
    public void Update()
    {
        if (cooldownReady <= 0 && !uIController.inventoryOpen && !menuOpen && !uIController.pauseMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TryUseItem();
                
            }
        }
        else
        {
            cooldownReady -= Time.deltaTime;
        }
        

    }
    public void TryUseItem()
    {
        Item item = inventorySlotPanel.uiItems[toolbarIndicator.indCurLoc].item;
        if (item != null)
        {

            switch (item.useType)
            {
                case UseType.Gift:
                    Debug.Log("We are giving a gift: " + item.title);
                    break;
                case UseType.Axe:
                    SwingAxe(item);
                    cooldownReady = cooldownTime;
                    break;
                case UseType.Hoe:
                    UseHoe();
                    cooldownReady = 1f;
                    break;
                case UseType.Pickaxe:
                    cooldownReady = cooldownTime;
                    break;
                case UseType.Seed:
                    TryPlantSeed(item);
                    break;
                case UseType.Default:
                    PickUpItem();
                    break;
                default:
                    break;
            }

        }
        else
        {
            PickUpItem();
        }
        
    }
    public void SwingAxe(Item item)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerMovement.facingDirection, playerMovement.rayLenght, targetMask);

        if (hit)
        {
            Interactable intr = hit.transform.GetComponent<Interactable>();
            if (intr != null)
            {
                if (item.stats.TryGetValue("STR", out int value))
                {
                    Interactable interactable = hit.transform.GetComponent<Interactable>();
                    interactable.Interac(value);
                    // Swing an axe animation
                    
                    animator.SetTrigger("Swing Axe");
                }
            }
        }

    }
    public void SwingWeapon(Item item)
    {

    }
    public void TryPlantSeed(Item item)
    {
        cropChecker.PlaceSeed(item);
        
    }
    public void UseHoe()
    {
        MousePosChecker();

        

        switch (direction)
        {
            case Direction.Up:
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                break;
            case Direction.Down:
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                break;
            case Direction.Left:
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
                break;
            case Direction.Right:
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", true);
                break;
            default:
                break;
        }
        animator.SetTrigger("SwingHoe");
        playerMovement.StopMovement();

        StartCoroutine(HoeTime());
    }
    IEnumerator HoeTime()
    {
        yield return new WaitForSeconds(0.3f);
        AudioManager.instance.PlayClip("HoeGround");
        cropChecker.UseThis();
        yield return new WaitForSeconds(0.7f);
        playerMovement.AlloweMovement();
    }

    public void PickUpItem()
    {
        MousePosChecker();
        
        pickupGameobject.SetActive(true);
        pickupGameobject.transform.position = point.position;
        StartCoroutine(ActiveTime());
    }
    IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(0.1f);
        pickupGameobject.SetActive(false);
    }



    public void MousePosChecker()
    {
        // Cheching mouseposition in screen
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToViewportPoint(Input.mousePosition).x, Camera.main.ScreenToViewportPoint(Input.mousePosition).y);

        // Anything that is 0 and above are top and left curters.
        float mpTotalX = mousePosition.y - mousePosition.x;

        // Anything that is 0 and above are top and right curters.
        float mpTotalY = (mousePosition.x - -(mousePosition.y + 1)) - 2;

        Vector3 centerPos = new Vector3(transform.position.x , transform.position.y, 0);
        
        // Our mouse is top or left of player.
        if (mpTotalX >= 0)
        {

            if (mpTotalY >= 0)
            {
                // Our mouse is top of player.
                point.position = new Vector3(centerPos.x, centerPos.y + toolPositionOffset, 0);
                direction = Direction.Up;
            }
            else
            {
                // Our mouse is left of player.
                point.position = new Vector3(centerPos.x - toolPositionOffset, centerPos.y, 0);
                direction = Direction.Left;
            }
        }
        else if (mpTotalY >= 0)
        {
            // Our mouse is Right of player.
            point.position = new Vector3(centerPos.x + toolPositionOffset, centerPos.y, 0);
            direction = Direction.Right;
        }
        else
        {
            // Our mouse is Down of player.
            point.position = new Vector3(centerPos.x, centerPos.y - toolPositionOffset, 0);
            direction = Direction.Down;
        }
        
    }

}
public enum Direction { Up, Down, Left, Right } 
