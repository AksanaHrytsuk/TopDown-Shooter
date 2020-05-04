using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : PickUp
{
    public float addMoney;

    public override void Apply()
    {
        GetPlayer().Money1 += addMoney;
    }
}
