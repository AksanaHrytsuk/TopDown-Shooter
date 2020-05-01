using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    [Header("Config Parameters")]
    public float health;
    public float speed;
    public float damage;


    private Player _player;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Animator _animator;
    
    public Player GetPlayer()
    {
        return _player;
    }

    public Collider2D GetCollider()
    {
        return _collider2D;
    }

    public Rigidbody2D GetRig()
    {
        return _rigidbody2D;
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public virtual void GetDamage(float getDamage)
    {
        health -= getDamage;
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        GetAnimator().SetTrigger("Death");
        Destroy(this);
        Destroy(GetCollider());
        GetRig().velocity = Vector2.zero;
    }
    // при соприкосновении объектов вызывает метод, наносящий урон у поставщика урона
    // @param collision - объект, который наносит урон 
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        DoDamage(collision.GetComponent<BaseClass>());
    }
    // заставляет получить урон
    // @parameter damageDealler - объект, который наносит урон
    public virtual void DoDamage(BaseClass damageDealler)
    {
        if (damageDealler.damage != null)
        {
            GetDamage(damageDealler.damage);
        }
    }

    public virtual void Move()
    {
    }

    public virtual void Rotate()
    {
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _animator = GetComponentInChildren<Animator>();
        _collider2D = GetComponent<Collider2D>();
        StartAdditional();
    }

    public virtual void StartAdditional()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}