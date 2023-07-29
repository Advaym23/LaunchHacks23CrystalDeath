using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{

    public float vertical;
    public float verticalSpeed = 5f;
    public bool isLadder;
    public bool isClimbing;

    Rigidbody2D rb;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }


    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * verticalSpeed);
        }
        else
        {
            rb.gravityScale = 1f;
        }
        //if(touchingDir.IsOnLadder)
        //{
        //    animator.SetTrigger("Jump");
        //    rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        //}
        //else
        //{
        //}
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Not touching ladder");
        if (collision.CompareTag("Ladder"))
        {

            Debug.Log("Touching ladder");
            isLadder = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touched ladder");
        if (collision.CompareTag("Ladder"))
        {

            Debug.Log("Touching ladder");
            isLadder = true;
            isClimbing = false;

        }
    }
}
