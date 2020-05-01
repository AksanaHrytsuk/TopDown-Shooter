using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationHelper : MonoBehaviour
{
    private Zombie _zombie;
    void Attack()
    {
       // _zombie.DoDamToPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        _zombie = GetComponentInParent<Zombie>();
    }
}
