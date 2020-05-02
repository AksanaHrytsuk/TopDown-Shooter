using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationHelper : BaseClass
{
    void Attack()
    {
        float zombieDamage = GetComponentInParent<Zombie>().damage;
        GetPlayer().GetDamage(zombieDamage);
    }
}
