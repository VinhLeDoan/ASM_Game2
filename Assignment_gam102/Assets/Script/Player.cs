﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Move")]
    public float moveSpeed;
    public float jumpForce;

    private int facingDirection = 1;
    private bool facingRight = true;
    private float xInput;

    [Header("Ground Check")]
    [SerializeField] private float groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Bullet")]
    public GameObject _bullet;
    public Transform _bulletPos;

    public int highScore;
    public TextMeshProUGUI highScorePanel;
    public Text highScoreText;
    public Text Diem;
    int Score = 0;


    public GameObject panelEndGame;


    // Start is called before the first frame update
    void Start()
    {
        //lấy highscore ra từ registry
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore.ToString();
        Diem = GameObject.Find("Diem").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FlipController();
        CheckInput();
        CollisionCheck();
        AnimatorController();
        Shoot();
        fallDead();

    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("va cham vao: " + collision.gameObject.tag);
        if (collision.CompareTag("Coin"))
        {
            Score++;
            collision.gameObject.SetActive(false);
            Diem.text = "Coin: " + Score.ToString();
        }
        else if(collision.gameObject.CompareTag("Bullet"))
        {
            Time.timeScale = 0f;
            panelEndGame.SetActive(true);
            SaveHighScore();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boar") || collision.gameObject.CompareTag("Flower"))
        {
            Time.timeScale = 0f;
            panelEndGame.SetActive(true);
            SaveHighScore();
        }
        
    }

    public void SaveHighScore()
    {
        if(Score > highScore)
        {
            highScore = Score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScorePanel.text = "High Score: " + highScore.ToString();
        }
    }

    public void fallDead()
    {
        if (transform.position.y < -10f)
        {

            panelEndGame.SetActive(true);
            SaveHighScore();
        }
    }

    public void RestartGame()
    {
        panelEndGame.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }



    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheck, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            panelEndGame.SetActive(true);
            SaveHighScore();
        }
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Jump()
    {
        if (isGrounded)   
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,
            transform.position.y - groundCheck));
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Score <= 0)
            {
                Score = 0;
                return;
            }
            else if (Score > 0) 
            {
                Instantiate(_bullet, _bulletPos.position, Quaternion.identity, transform);
                Score --;
                Diem.text = "Coin: " + Score.ToString();
            }
        }
    }
}
