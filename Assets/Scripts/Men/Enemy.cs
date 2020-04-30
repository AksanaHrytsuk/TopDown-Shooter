using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPosition;
   
    public float fireRate;
    public float health;
    public float delay;
    public float rate;
    
    private float nextFire;
   
    private Animator _animator;

    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        StartCoroutine(CreateBullets(delay, rate)); 
    }

    IEnumerator CreateBullets(float delay, float rate)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        { 
            Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
            _animator.SetTrigger("Shoot");
            // nextFire = fireRate;
            //
            // if (nextFire > 0)
            // {
            //     nextFire -= Time.deltaTime;
            // }
            yield return new WaitForSeconds(rate);
        }
        
    }
    void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            _animator.SetTrigger("Death");
            
            Destroy(this);

            EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
            Destroy(enemyMovement);

            Collider2D collider = GetComponent<Collider2D>();
            Destroy(collider);
            //Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
            // health -= damageDealer.damage;
            DoDamage(damageDealer.damage);
    }
}
