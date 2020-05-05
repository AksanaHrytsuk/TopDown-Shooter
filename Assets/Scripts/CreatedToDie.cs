using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CreatedToDie : BaseClass
{
 public override void Death()
 {
  Destroy(gameObject);
 }
}
