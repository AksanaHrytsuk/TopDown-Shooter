using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChest : PickUp
{
    
    public float addHealth;
    public override void Apply()
    {
        if (GetPlayer().health + addHealth <= GetPlayer().maxHealth)
        {
            GetPlayer().health += addHealth;
        }
        else
        {
            GetPlayer().health = GetPlayer().maxHealth;
        }
    }
}
