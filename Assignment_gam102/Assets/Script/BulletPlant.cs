using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-5f, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {        
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Time.timeScale = 0.0f;
        }
        else if(collision.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
