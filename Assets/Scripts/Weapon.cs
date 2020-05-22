using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

using Lean.Pool;
public class Weapon : MonoBehaviour
{
    [Header("Elements UI")]
    public GameObject bulletPrefab;
    public Transform shootPosition;
    public Text ammo;
    
    [Header("Config parameters")]
    public float fireRate;

    public int amountBullets;
    public int maxAmountBullets;
   
    private float _nextFire;
    private Animator _animator;
    private bool _canShoot = true;

    public float GetNextFire()
    {
        return _nextFire;
    }

    public bool GetCanShoot()
    {
        return _canShoot;
    }

    public void SetCanShoot(bool canShoot)
    { 
        _canShoot = canShoot;
    }

    public void SetNextFire(float number)
    {
        _nextFire = number;
    }
    
    public Animator GetAnimator()
    {
        return _animator;
    }
    
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Shoot();
        ammo.text = "Ammo: " + amountBullets;
    }

    public  void Shoot()
    {
        if (ShootPossibility())
        {
            LeanPool.Spawn(bulletPrefab, shootPosition.position, transform.rotation);
            _nextFire = fireRate;
            _animator.SetTrigger("Shoot");
            amountBullets--;
        }
        if (_nextFire > 0)
        {
            _nextFire -= Time.deltaTime;
        }
    }

    public virtual bool ShootPossibility()
    {
        return (Input.GetButton("Fire1") && _nextFire <= 0 && _canShoot && amountBullets > 0);
    }
}
