using UnityEngine;
using Lean.Pool;

public class Zombie : BaseClass
{
    [Header("Attack config")]
    public float attackRate;
    private float nextAttack;
    
    [Header("AI config")] public float followDistance;
    public float attackDistance;
    public float loseDistance;
    public float probability;
    public GameObject[] pickUps;
    
    public float searchAngel = 45f;
    enum ZombieStates
    {
        Patrol,
        Attack,
        Follow
    }

    private ZombieStates activeState;

    public override void StartAdditional()
    {
        ChangeState(ZombieStates.Patrol); // состояние зомби при старте игры Patrol(патрулирует)
    }
    
    void Update()
    {
        if (PlayerIsDead())
        {
            ChangeState(ZombieStates.Patrol);
        }
        else
        {
            UpdateState();
        }
        
        if (GetRig() != null)
        {
            GetAnimator().SetFloat("Speed", GetRig().velocity.magnitude);
        }
    }

    void UpdateState()
    {
        if (GetPlayer() != null)
        {
            float distance = Vector3.Distance(transform.position, GetPlayer().transform.position); // дистанция от зомби к игроку

            switch (activeState)
            {
                
                case ZombieStates.Patrol: 
                    if (distance < followDistance)
                    {
                        // направление от игрока к зомби
                        Vector3 direction = GetPlayer().transform.position - transform.position;
                        // угол между двумя векторами 
                        float angel = Mathf.Abs(Vector3.Angle(direction, -transform.up));
                        
                        LayerMask layerMask = LayerMask.GetMask("Walls");
                        Debug.Log("search angel");

                        if (angel <= searchAngel)
                        {
                            Debug.Log("pool reycast");
                            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layerMask);
                            
                            if (hit.collider == null)
                            {
                                Debug.Log("Follow");
                                ChangeState(ZombieStates.Follow);
                            }
                        }
                    }
                    break;
                case  ZombieStates.Follow:
                    if (distance < attackDistance)
                    {
                        Debug.Log("Attack");
                        ChangeState(ZombieStates.Attack);
                    }
                    else if (distance > loseDistance) 
                    {
                        ChangeState(ZombieStates.Patrol);
                    }

                    Rotate();
                    
                    break;
                // case ZombieStates.Move: // если activeState == ZombieStates.Move , зомби движется в направлении игрока (Rotate()) И
                //     if (distance < attackDistance) // если дистанция от зомби до игрока меньше или равна attackDistance, то активировать атаку у зомби 
                //     {
                //         ChangeState(ZombieStates.Attack);
                //     }
                 
                case ZombieStates.Attack:
                    
                    if (distance > attackDistance)
                    {
                        ChangeState(ZombieStates.Follow);
                    }
                    Rotate();
                    nextAttack -= Time.fixedDeltaTime;
                    if (nextAttack <= 0)
                    {
                         GetAnimator().SetTrigger("Shoot");
                         nextAttack = attackRate;
                    }
                    GetAnimator().SetTrigger("Shoot"); // включать триггер Шот(Аттака) - проигрывается анимация атаки   
                    break;
            }
        } 
    }

    private bool PlayerIsDead()
    {
        return GetPlayer().health <= 0;
    }

    void ChangeState(ZombieStates newState) 
    {
        activeState = newState;
        
        switch (activeState) 
        {
            case ZombieStates.Follow:
                // _zombieMovement.enabled = false;
                // SetDoMove(true);
                // SetDoFollow(true);
                break;
            case ZombieStates.Patrol:
                // SetDoMove(true);
                // SetDoFollow(false);
                break;
            // case ZombieStates.Move: // если активный шаг == Мув, то включить движение зомби 
            //     SetDoMove(true);
            //     break;
            case ZombieStates.Attack: // если активный шаг == Аттак, то отключить движение зомби 
                // SetDoMove(false);
                // StopMovement();
                break;
        } 
    }

    public override void Death()
    {
        base.Death();
        Destroy(this);
        CreatePickUp();
    }
   
    public void CreatePickUp()
    {
        if (Chance())
        {
            {
                LeanPool.Spawn(pickUps[0], transform.position, Quaternion.identity);
            }
        }
    }

    bool Chance()
    {
        int chance = Random.Range(1, 100);
        if (chance < probability)
        {
            return true;
        }

        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, followDistance); 
        
        Gizmos.DrawWireSphere(position, attackDistance);
        Gizmos.color = Color.yellow;
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(position, loseDistance);

        Gizmos.color = Color.magenta;
        Vector3 lookDirection = -transform.up;
        Gizmos.DrawRay(transform.position, lookDirection * followDistance);
        Gizmos.color = Color.green;
        //Quaternion rotation = Quaternion.AngleAxis(searchAngel, Vector3.forward);
        Vector3 v1 = Quaternion.AngleAxis(searchAngel, Vector3.forward) * lookDirection;
        Vector3 v2 = Quaternion.AngleAxis(-searchAngel, Vector3.forward) * lookDirection;
        Gizmos.DrawRay(transform.position, v1 * followDistance);
        Gizmos.DrawRay(transform.position, v2 * followDistance);
    }
}
