using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Player _player;

    public Player GetPlayer()
    {
        return _player;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Characters"))
        {
         Apply();
         Destroy(gameObject);
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        StartIsChanged();
    }

    public virtual StartIsChanged()
    {
        
    }

    public virtual void Apply()
    {
        
    }
}
