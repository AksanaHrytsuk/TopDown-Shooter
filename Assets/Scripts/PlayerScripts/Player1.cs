using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [Header("Elements UI")]
    public GameObject bulletPrefab;
    public Transform shootPosition;

    [Header("Config parameters")]
    public float fireRate;
    public float health;
    public float delay;
    public float rate;

    private float _nextFire;
    private Animator _animator;
    private Collider2D _collider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _nextFire <= 0)
        {
            Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
            _nextFire = fireRate;
            _animator.SetTrigger("Shoot");
        }
        if (_nextFire > 0)
        {
            _nextFire -= Time.deltaTime;
        }
    }

    public void DoDamage(int damage )
    {
        health-= damage;
        if (health <= 0)
        {
            _animator.SetTrigger("Death");
            Destroy(this);

            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            Destroy(playerMovement);

            Collider2D collider2D = GetComponent<Collider2D>();
            Destroy(collider2D);

            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(DoDamageInTime(delay, rate, collision));
        // if (this.GetComponent<Collider2D>().IsTouching(collision.GetComponent<Collider2D>()))
        // {
        //     DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        //
        //     if (damageDealer != null)
        //         DoDamage(damageDealer.damage);
        // }
    }

    IEnumerator DoDamageInTime(float delay, float rate, Collider2D collision)
    {
        yield return new WaitForSeconds(delay);

        while (CheckIfTouching(collision))
        {
            GetDamage(collision);
            yield return new WaitForSeconds(rate);
        }
    }

    bool CheckIfTouching(Collider2D collision)
    {
        return _collider2D.IsTouching(collision);
    }
    void GetDamage(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            DoDamage(damageDealer.damage);
        }
    }
}
