using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseClass
{
     [Header("Config parameters")]
        public float speed;
         
        private Player _player;
        private Rigidbody2D rb;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>(); // компонент у зомби
        }
        void Start()
        {
            _player = FindObjectOfType<Player>(); // объект _player на сцене
        }
        void FixedUpdate()
        {
            if (_player != null)
            {
                Vector2 direction = _player.transform.position - transform.position; // желаемое - текущее
    
                rb.velocity = direction.normalized * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        public void StopMovement() 
        {
            rb.velocity = Vector2.zero;
        }
}
