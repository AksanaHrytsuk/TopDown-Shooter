using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyMovement : BaseClass
{
    [Header("AI config")]
    public float followDistance;
    public float attackDistance;
    public float loseDistance;
    public float distanceToPoint;
    public float probability;
    public GameObject[] patrolPoints;
    public GameObject[] pickUps;

    private MedicineChest _medicineChest;
    
    private bool _doMove;
    private bool _doFollow;
    
    public bool GetDoMove()
    {
        return _doMove;
    }

    public void SetDoFollow(bool c)
    {
        _doFollow = c;
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

    public override void StartAdditional()
    {
        _medicineChest = FindObjectOfType<MedicineChest>();
    }

    private Vector2 Direction()
    {
        if (_doFollow)
        {
            return GetPlayer().transform.position - transform.position;
        }

        if (patrolPoints.Length == 0)
        {
            StopMovement();
            return transform.position;
        }
        Vector2 direction =  patrolPoints[0].transform.position - transform.position; // желаемое - текущее 
        ChangeDirection();
        return direction;
    }

    private void ChangeDirection()
    {
        float distance = Vector2.Distance(transform.position, patrolPoints[0].transform.position);
        if (distance < distanceToPoint)
        {
            ChangeArray();
        }
    }

    private void ChangeArray()
    {
        GameObject tmp = patrolPoints[0];
        for (int i = 0; i < patrolPoints.Length - 1; i++)
        {
            patrolPoints[i] = patrolPoints[i + 1];
        }

        patrolPoints[patrolPoints.Length-1] = tmp; // обращение к последнему элементу массива
    }
    public override void Move()
    {    
        if  (GetPlayer() != null && _doMove)
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
    public void CreatePickUp()
    {
        if (_medicineChest != null)
            if (Chance())
            {
                {
                    Instantiate(pickUps[0], transform.position, Quaternion.identity);
                }
            }
    }
    
    bool Chance()
    {
        int chance = Random.Range(1, 100);
        if (chance < probability)
        {
            return true;
        }
        return false;
    }
}