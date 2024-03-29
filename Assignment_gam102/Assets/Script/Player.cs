using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Text Diem;
    int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
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
}
