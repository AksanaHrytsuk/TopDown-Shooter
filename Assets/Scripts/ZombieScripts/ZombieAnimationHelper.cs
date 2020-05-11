using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationHelper : BaseClass
{
    public AudioClip zombieSound;
   public void Attack()
    {
        PlaySound(zombieSound);
        Zombie thisZombie = GetComponentInParent<Zombie>();
        if (thisZombie != null)
        {
            float zombieDamage = thisZombie.damage;
            GetPlayer().GetDamage(zombieDamage);
        }
    }
}
