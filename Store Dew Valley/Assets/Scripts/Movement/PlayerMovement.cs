using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public event Action OnPlayerFlip;

    public float moveSpeed = 10f;
    Vector2 movement;
    private bool canMove = true;

    [Header("Raycasting")]
    [HideInInspector]
    public Vector2 facingDirection;
    [HideInInspector]
    public float rayLenght;
    // Components
    Rigidbody2D rb;
    public Animator animator;

    [HideInInspector]
    public static PlayerMovement instance;

    public Directions directions = Directions.Down;


    private bool m_FacingRight = true;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (FindObjectsOfType<PlayerMovement>().Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        animator.SetFloat("Speed", movement.magnitude);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);


        Vector2 vel = new Vector2(movement.x, movement.y);
        if (vel.magnitude != 0)
            facingDirection = vel;

        PlayerAnimations(vel, movement.normalized);


        Debug.DrawRay(transform.position, facingDirection * rayLenght, Color.red);
    }

    private void FixedUpdate()
    {
        Vector2 direction = movement.normalized;
        Vector2 velocity = direction * moveSpeed;
        Vector2 moveAmount = velocity * Time.deltaTime;

        transform.position += new Vector3(moveAmount.x, moveAmount.y, 0);


        


        // rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        /*
        // If the input is moving the player right and the player is facing left...
        if (movement.x > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movement.x < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }*/
    }

    public void TeleportTo(Vector2 position)
    {
        transform.position = position;
    }
    public void StopMovement()
    {
        canMove = false;
        movement.x = 0;
        movement.y = 0;
    }
    public void AlloweMovement()
    {
        canMove = true;
    }

    private void Flip()
    {
        if (OnPlayerFlip != null)
        {
            OnPlayerFlip();
        }

        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void PlayerAnimations(Vector2 velocity, Vector2 direction)
    {
        // We are moving
        if (velocity.magnitude != 0)
        {

            // We are not moving Left or Right
            if (direction.x > 0)
            {
                directions = Directions.Right;

            }
            else if (direction.x < 0)
            {
                directions = Directions.Left;
            }

            // We are moving Up or down
            if (direction.y > 0)
            {
                directions = Directions.Up;
            }
            else if (direction.y < 0)
            {
                directions = Directions.Down;
            }
        }
        else
        {
            return;
        }
        switch (directions)
        {
            case Directions.Up:
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                break;
            case Directions.Down:
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                break;
            case Directions.Left:
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
                break;
            case Directions.Right:
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", true);
                break;
            default:
                break;
        }

    }
}
public enum Directions { Up, Down, Left, Right }
