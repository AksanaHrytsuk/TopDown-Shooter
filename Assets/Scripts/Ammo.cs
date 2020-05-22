using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class Ammo : PickUp
{
    [Header("Config Elements")] public int addAmmo;

    
    public override void Apply()
    {
        if (Weapon.amountBullets + addAmmo <= Weapon.maxAmountBullets)
        {
            Weapon.amountBullets += addAmmo;
        }
        else
        {
            Weapon.amountBullets = addAmmo;

        }
    }
}
