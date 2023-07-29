using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 5f;

    public enum WalkableDirection { Right, Left };

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection { get
        {
            return _walkDirection;
        } set {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.Log("in Fixed updated monster");
            rb.velocity = new Vector2(walkDirectionVector.x * speed, rb.velocity.y);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision enter");
        if (collision.gameObject.CompareTag("barrier")) {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
            Debug.Log("Going left");
        } else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
            Debug.Log("Going Right");
        }
        else
        {
            Debug.LogError("Monster not proper direction");
        }
    }
}
