using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour
{
    [Header("Config Parameters")] public float health;
    public float speed;
    public float damage;


    private Player _player;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;


    public Player GetPlayer()
    {
        return _player;
    }

    public Rigidbody2D GetRig()
    {
        return _rigidbody2D;
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public virtual void GetDamage(float damage)
    {
    }

    public virtual void DoDamage(BaseClass obj)
    {
        obj.GetDamage(damage);
    }

    public virtual void Move()
    {
    }

    public virtual void Rotate()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}