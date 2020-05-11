using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    public Action onHealthChanged = delegate {  };
    
    [Header("Config Parameters")]
    public float health;
    public float speed;
    public float damage;
    public float Money1 { get; set; }

    private AudioSource _audioSource;
    private Player _player;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Animator _animator;
    private Weapon _weapon;
    
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

    public Weapon GetWeapon()
    {
        return _weapon;
    }

    public virtual void GetDamage(float getDamage)
    {
        health -= getDamage;
        
        onHealthChanged();
        
        if (health <= 0)
        {
            Death();
        }
    }

    // проигрывается анимация Death, уничтожается компонент,  коллайдер объекта, остановка движения объекта
    public virtual void Death()
    {
        GetAnimator().SetTrigger("Death");
        // Destroy(this);
        Destroy(GetCollider());
        Destroy(GetRig());
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
        _weapon = GetComponent<Weapon>();
        _audioSource = GetComponent<AudioSource>();
        StartAdditional();
    }

    public virtual void StartAdditional()
    {
        
    }
    
    public void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }
}