using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : PickUp
{
    public Text money;
    public float addMoney;

    public override void Apply()
    {
        GetPlayer().Money1 += addMoney;
        money.text = "$ " + GetPlayer().Money1;
    }
}
