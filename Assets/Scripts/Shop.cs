using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Characters"))
        {
           shopPanel.SetActive(true);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(false);
        shopPanel = FindObjectOfType<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
