using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class PlayerController : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    Vector2 moveInput;


    public Rigidbody2D coin;

    private List<Tuple<float, float>> tuples = new List<Tuple<float, float>>();

    public float walkSpeed = 5f;
    public float runSpeed = 5f;

    public bool _isMoving = false;

    public bool _isRunning = false;

    public bool _isLadderClimbing = false;

    public int life = 100;

    public int coinsPoints = 0;

    private bool _dead = false;

    TouchingDirection touchingDir;

    public bool coinAvailable = false;

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0;
            }
        }
    }
    Rigidbody2D rb;
    Animator animator;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool("IsRunning", value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public float jumpImpulse = 10f;

    private void Start()
    {
        UpdateHealth();
        UpdateScore();
    }

    private void Awake()
    {
        tuples.Add(new Tuple<float, float>(-6.4f, -490.0f));
        tuples.Add(new Tuple<float, float>(7.22f, -489));
        tuples.Add(new Tuple<float, float>(32f, -487.45f));
        tuples.Add(new Tuple<float, float>(23.7f, -483.24f));
        tuples.Add(new Tuple<float, float>(8.67f, -483.34f));
        tuples.Add(new Tuple<float, float>(-4.17f, -481f));
        tuples.Add(new Tuple<float, float>(11f, -473.28f));
        tuples.Add(new Tuple<float, float>(25.41f, -475f));
        tuples.Add(new Tuple<float, float>(21f, -467.95f));
        tuples.Add(new Tuple<float, float>(1.35f, -465f));

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDir = GetComponent<TouchingDirection>();
    }

    private void FixedUpdate()
    {
        if (!coinAvailable)
        {
            //foreach(var pair in tuples)
            {
                int x = UnityEngine.Random.Range(1, 10);
                Vector3 v3 = Vector3.zero;
                v3.x = tuples[x].Item1;
                v3.y = tuples[x].Item2;
                var spawnBullet = Instantiate(coin, v3, Quaternion.identity);

            }
            coinAvailable = true;
        }

        if (!_dead)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
            animator.SetFloat("YVelociy", rb.velocity.y);
        }

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spike"))
        {
            killPlayer();
        }
        else if (collision.gameObject.CompareTag("bullet"))
        {
            life -= 5;
            if (life < 0)
            {
                life = 0;
                killPlayer();
            }
            UpdateHealth();
        }
        else if (collision.gameObject.CompareTag("killerbullet"))
        {
            life -= 25;
            if (life < 0)
            {
                life = 0;
                killPlayer();
            }
            UpdateHealth();
        }
        else if (collision.gameObject.CompareTag("coin"))
        {
            Debug.Log("Touched coin");
            coinsPoints++;
            coinAvailable = false;
            UpdateScore();
        }

        //Debug.Log("collision enter");
    }

    private void UpdateScore()
    {
        scoreText.text = "Coins " + coinsPoints.ToString();
    }

    private void UpdateHealth()
    {
        healthText.text = "Health " + life.ToString();
    }

    public void killPlayer()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 3);
        _dead = true;
        //SceneManager.LoadScene(1);
        StartCoroutine(ExecuteAfterTime());
        //reloadGame();
    }

    IEnumerator ExecuteAfterTime()
    {
        Debug.Log("Now waiting");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
        // Code to execute after the delay
    }

    private void reloadGame()
    {
        SceneManager.LoadScene(1);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    Debug.Log("Dying......");
    //    if(collision.CompareTag("spike"))
    //    {
    //        animator.SetTrigger("Die");
    //    }
    //}

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //Check if Alive as well
        if (context.started && touchingDir.IsGrounded)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

}
