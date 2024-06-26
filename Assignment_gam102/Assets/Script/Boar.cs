﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : MonoBehaviour
{
    private int _direction; //1. right  -1. left
    private Rigidbody2D _rb;
    public float _speedBoar;


    int mau = 2;

    void Start()
    {
        _direction = -1;
        _speedBoar = 1;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector3(_speedBoar * _direction, _rb.velocity.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fence"))
        {
            _direction *= -1; //Đổi hướng
            _rb.gameObject.transform.localScale = new Vector3(_rb.gameObject.transform.localScale.x * -1, 1, 1);
        }
        if (collision.gameObject.CompareTag("BulletCoin"))
        {
            mau--;
            Destroy(collision.gameObject);
            if (mau <= 0)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.0f;
        }
    }
}
