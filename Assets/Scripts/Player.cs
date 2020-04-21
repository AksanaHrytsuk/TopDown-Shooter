using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform shootPosition;

    public float fireRate;

    private float health = 100;

    private float nextFire;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && nextFire <= 0)
        {
            Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
            nextFire = fireRate;
            _animator.SetTrigger("Shoot");
        }

        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
    }

    private void DoDamage(int damage)
    {
        health-= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
            DoDamage(damageDealer.damage);
    }
}
