using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed;
     
    private Player _player;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // компонент у зомби
    }
    void Start()
    {
        _player = FindObjectOfType<Player>(); // объект _player на сцене
    }

    // Update is called once per frame
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
