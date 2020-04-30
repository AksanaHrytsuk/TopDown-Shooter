using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player _player;
  
    // Start is called before the first frame update
    void Start()
    {
       _player = FindObjectOfType<Player>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    
    private void Rotate()
    {
        if (_player != null)
        {
            Vector2 direction = transform.position - _player.transform.position;
            transform.up = direction;
        }
    }
}
