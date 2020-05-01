using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Elements UI")]
    public GameObject bulletPrefab;
    public Transform shootPosition;
    
    [Header("Config parameters")]
    public float fireRate;
   
    private float _nextFire;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetButton("Fire1") && _nextFire <= 0)
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
}
