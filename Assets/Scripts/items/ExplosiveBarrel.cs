using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : BaseClass
{
    [Header("Config parameters")] public bool explosive;
    public float explodeRadius;

    public override void Death()
    {
        base.Death();
        if (explosive)
        {
            //find objects in radius
            Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
            foreach (Collider2D objectI in objectsInRadius)
            {
                if (objectI.gameObject == gameObject)
                {
                    continue; //the same gameObject ==> next iteration
                }

                BaseClass damageOwner = objectI.GetComponent<BaseClass>();
                if (damageOwner == null)
                {
                    Destroy(damageOwner);
                }
                else
                {
                    damageOwner.GetDamage(damage);
                    Destroy(gameObject,1);
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}