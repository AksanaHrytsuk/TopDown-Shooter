using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseClass
{
    // Start is called before the first frame update
    public override void StartAdditional()
    {
        GetRig().velocity = -transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
