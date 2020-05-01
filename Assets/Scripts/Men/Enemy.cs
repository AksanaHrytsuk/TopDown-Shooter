using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseClass
{
    public GameObject bulletPrefab;
    public Transform shootPosition;
   
    public float fireRate;
    public float delay;
    public float rate;
    
    private float nextFire;

    // Start is called before the first frame update
    public override void StartAdditional()
    {
        StartCoroutine(CreateBullets(delay, rate));
    }

    IEnumerator CreateBullets(float delay, float rate)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        { 
            Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
            GetAnimator().SetTrigger("Shoot");
            yield return new WaitForSeconds(rate);
        }
        
    }
    // void DoDamage(float damage)
    // {
    //     health -= damage;
    //     if (health <= 0)
    //     {
    //         GetAnimator().SetTrigger("Death");
    //         
    //         Destroy(this);
    //
    //         EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
    //         Destroy(enemyMovement);
    //
    //         Collider2D collider = GetComponent<Collider2D>();
    //         Destroy(collider);
    //         //Destroy(gameObject);
    //     }
    // }
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
    //
    //     if (damageDealer != null)
    //         // health -= damageDealer.damage;
    //         DoDamage(damageDealer.damage);
    // }
}
