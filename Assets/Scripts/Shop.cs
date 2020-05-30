using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Player player;
    public GameObject shopPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Characters"))
        {
           shopPanel.SetActive(true);
           Time.timeScale = 0;
           player.enabled = false;
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        shopPanel.SetActive(false);
    }
    
    public void Resume()
    {
            player.enabled = true;
            shopPanel.SetActive(false);
            Time.timeScale = 1;
    }
    
    
}
