using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
 public override bool ShootPossibility()
 {
  return GetNextFire() <= 0 && GetCanShoot();
 }
}
