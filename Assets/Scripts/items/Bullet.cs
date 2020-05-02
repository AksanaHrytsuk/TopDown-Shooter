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
    
    // при соприкосновении объектов вызывает метод, наносящий урон тому с кем сталкивается
    // @param collision - объект, который получает урон
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseClass damageOwner = collision.GetComponent<BaseClass>();
        if (damageOwner != null)
        {
            damageOwner.GetDamage(damage);
        }
        Destroy(gameObject);
    }

}
