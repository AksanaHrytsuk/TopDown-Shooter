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


    public override void Move()
    {    
        if (GetPlayer() != null && _doMove )
        {
            Vector2 direction = GetPlayer().transform.position - transform.position; // желаемое - текущее

            GetRig().velocity = direction.normalized * speed;
        }
        else
        {
            GetRig().velocity = Vector2.zero;
        }
    }

    public override void Rotate()
    {
        if (GetPlayer() != null)
        {
            Vector2 direction = GetPlayer().transform.position - transform.position;
            transform.up = -direction;
        }
    }
    
    public void StopMovement()
    {
        GetRig().velocity = Vector2.zero;
    }
}