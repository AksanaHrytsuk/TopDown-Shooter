using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform shootPosition;
    
    public float fireRate = 0.3f;
    
    public float health = 100;
    
    private float nextFire;

    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateBullets(4, 2)); 
    }

    IEnumerator CreateBullets(float delay, float rate)
    {
        //Debug.Log("Create");
        yield return new WaitForSeconds(delay);

        while (true)
        { 
            //Debug.Log("Create bullets");
            Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
            nextFire = fireRate;

            if (nextFire > 0)
            {
                nextFire -= Time.deltaTime;
            }
            yield return new WaitForSeconds(rate);
        }
        
    }
    void DoDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
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
