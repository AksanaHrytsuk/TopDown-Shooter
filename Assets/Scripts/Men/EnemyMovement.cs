using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BaseClass
{
    [Header("AI config")]
    public float followDistance;
    public float attackDistance;
    public float loseDistance;
    private bool _doMove;

    public bool GetDoMove()
    {
        return _doMove;
    }

    public void SetDoMove(bool b)
    {
        _doMove = b;
    }
    void FixedUpdate()
    {
        Move();
        Rotate();
        FUpdate();
    }

    public virtual void FUpdate()
    {
    }

    private Vector2 Direction()
    {
        return GetPlayer().transform.position - transform.position; // желаемое - текущее 
    }
    public override void Move()
    {    
        if (GetPlayer() != null && _doMove )
        {
            GetRig().velocity = Direction().normalized * speed;
        }
        else
        {
            StopMovement();
        }
    }

    public override void Rotate()
    {
        if (GetPlayer() != null)
        {
            transform.up = -Direction();
        }
    }
    
    public void StopMovement()
    {
        if (GetRig() != null)
        {
            GetRig().velocity = Vector2.zero;
        }
    }
}