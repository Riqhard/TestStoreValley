using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphixController : MonoBehaviour
{
    public SpriteRenderer playerSpriterenderer;

    private float horizontalAxis;

    private bool facingLeft;

    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (horizontalAxis < -0.1f)
        {
            if (!facingLeft)
            {
                FlipGraphix();
                facingLeft = true;
            }
        }
        else if (horizontalAxis > 0.1f)     
        {
            if (facingLeft)
            {
                FlipGraphix();
                facingLeft = false;
            }
        }
    }
    public void FlipGraphix()
    {
        playerSpriterenderer.flipX = !playerSpriterenderer.flipX;
    }
}
