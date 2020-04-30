using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [Header("Config parameters")] 
    public bool explosive;
    public float explodeRadius;
    
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DestroyCharacters()
    {
        Debug.Log("Here1");
        if (explosive)
        {
            //find objects in radius
            Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, explodeRadius);

            Debug.Log(objectsInRadius.Length);
            Debug.Log("Here2");

            foreach (Collider2D objectI in objectsInRadius)
            {
                Debug.Log("Here loop");
                if (objectI.gameObject == gameObject)
                {
                    continue; //the same gameObject ==> next iteration
                }

                // Zombie block = objectI.gameObject.GetComponent<Zombie>();
                
                Player _player = objectI.gameObject.GetComponent<Player>();
                if (_player == null)
                {
                    Destroy(_player);
                }
                else
                {
                    _player.DoDamage(20);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetTrigger("Barrel");
        DestroyCharacters();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}