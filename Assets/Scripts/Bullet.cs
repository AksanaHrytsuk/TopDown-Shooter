using UnityEngine;

public class Bullet : BaseClass
{
    public override void StartAdditional()
    {
        
    }

    private void OnEnable()
    {
        GetRig().velocity = -transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf) // check if object was dispawned
        {
            Lean.Pool.LeanPool.Despawn(gameObject);
        }
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
        Lean.Pool.LeanPool.Despawn(gameObject);
    }

}
