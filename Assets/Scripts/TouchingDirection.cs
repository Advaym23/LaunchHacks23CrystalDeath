using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    //Rigidbody2D rb;

    CapsuleCollider2D touchingCol;
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float ladderDistance = 0.02f;

    Animator animoator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] ladderHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded = true;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animoator.SetBool("IsGrounded", value);
        }
    }

    [SerializeField]
    private bool _isOnLadder = true;
    //private Vector2 ladderCheckDirction => gameObject.transform.localScale.x > 0?Vector2.up:Vector2.;

    public bool IsOnLadder
    {
        get
        {
            return _isOnLadder;
        }
        private set
        {
            _isOnLadder = value;
            animoator.SetBool("IsOnLadder", value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animoator = GetComponent<Animator>();   
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;

        //IsOnWall = touchingCol.Cast(checkWallDirection, castFilter, ladderHits, ladderDistance) > 0;
        //IsOnLadder = touchingCol.Cast(Vector2.up, castFilter, ladderHits, ladderDistance) > 0;
    }
}
