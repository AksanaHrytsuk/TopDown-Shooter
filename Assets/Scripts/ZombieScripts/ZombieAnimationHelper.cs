using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationHelper : BaseClass
{
    public AudioClip zombieSound;
    void Attack()
    {
        PlaySound(zombieSound);
        float zombieDamage = GetComponentInParent<Zombie>().damage;
        GetPlayer().GetDamage(zombieDamage);
    }
}
