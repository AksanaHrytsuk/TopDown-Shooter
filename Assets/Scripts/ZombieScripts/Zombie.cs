using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("AI config")]
    public float followDistance;
    public float attackDistance;
    public float loseDistance;

    [Header("Attack config")]
    public float attackRate;
    public int damage;

    private ZombieMovement _zombieMovement;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private float nextAttack;
    
    enum ZombieStates
    {
        Stand,
        Move,
        Attack
    }

    private ZombieStates activeState;

    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();

        _rigidbody2D = GetComponent<Rigidbody2D>();

        _animator = GetComponentInChildren<Animator>();

        _zombieMovement = GetComponent<ZombieMovement>();
        
        ChangeState(ZombieStates.Stand); // состояние зомби при старте игры stand(стоит на месте)
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateState();
        
        _animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
    }

    void UpdateState()
    {
        if (_player != null)
        {
            float distance = Vector3.Distance(transform.position, _player.transform.position); // дистанция от зомби к игроку


            switch (activeState)
            {
                case ZombieStates.Stand: // если activeState == ZombieStates.Stand , зомби стоит на месте И
                    if (distance <= followDistance) // если дистанция от зомби до игрока меньше followDistance, то активировать движение у зомби
                    {
                        ChangeState(ZombieStates.Move);
                    }

                    break;
                case ZombieStates.Move
                    : // если activeState == ZombieStates.Move , зомби движется в направлении игрока (Rotate()) И
                    if (distance <= attackDistance) // если дистанция от зомби до игрока меньше или равна attackDistance, то активировать атаку у зомби 
                    {
                        ChangeState(ZombieStates.Attack);
                    }
                    else if (distance >= loseDistance) // иначе если дистанция от зомби до игрока больше или равна loseDistance зомби стоит на месте, игрок сбежал
                    {
                        ChangeState(ZombieStates.Stand);
                    }

                    Rotate();
                    break;
                case ZombieStates.Attack: // если activeState == ZombieStates.Attack , зомби движется в направлении игрока (Rotate()) И
                    if (distance > attackDistance) // если дистанция от зомби до игрока больше attackDistance, то активировать Движение у зомби 
                    {
                        ChangeState(ZombieStates.Move);
                    }
                    Rotate();

                    nextAttack -= Time.fixedDeltaTime;
                    if (nextAttack <= 0)
                    {
                        _animator.SetTrigger("Shoot");

                        nextAttack = attackRate;
                    }
                    _animator.SetTrigger("Shoot"); // включать триггер Шот(Аттака) - проигрывается анимация атаки
                    break;
            }
        } 
    }

    void ChangeState(ZombieStates newState) 
    {
        activeState = newState;
        
        switch (activeState) 
        {
            case ZombieStates.Stand: // если активный шаг == Стэнд, то отключить движение зомби 
                _zombieMovement.enabled = false;
                _zombieMovement.StopMovement();
                break;
            case ZombieStates.Move: // если активный шаг == Мув, то включить движение зомби 
                _zombieMovement.enabled = true;
                break;
            case ZombieStates.Attack: // если активный шаг == Аттак, то отключить движение зомби 
                _zombieMovement.enabled = false;
                _zombieMovement.StopMovement();
                break;
        } 
    }

    void Rotate()
    {
        Vector2 direction =  _player.transform.position - transform.position;
        transform.up = -direction;
    }

   public void DoDamToPlayer()
    {
        if (_player != null)
        {
            _player.DoDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, followDistance); 
        
        Gizmos.DrawWireSphere(position, attackDistance);
        Gizmos.color = Color.yellow;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(position, loseDistance);
    }
}
