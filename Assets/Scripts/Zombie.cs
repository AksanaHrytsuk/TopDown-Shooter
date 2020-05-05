using UnityEngine;

public class Zombie : EnemyMovement
{
    [Header("Attack config")]
    public float attackRate;

    public float probability;

    private float nextAttack;

    private MedicineChest _medicineChest;
    
    enum ZombieStates
    {
        Patrol,
        Attack,
        Follow
    }

    private ZombieStates activeState;

    // Start is called before the first frame update
    public override void StartAdditional()
    {
        ChangeState(ZombieStates.Patrol); // состояние зомби при старте игры Patrol(патрулирует)
        _medicineChest = FindObjectOfType<MedicineChest>();
    }

    public override void FUpdate()
    {
        UpdateState();
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
                        ChangeState(ZombieStates.Follow);
                    }
                    break;
                case  ZombieStates.Follow:
                    if (distance < attackDistance) 
                    {
                        ChangeState(ZombieStates.Attack);
                    }
                    else if (distance > loseDistance
                    ) 
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

    void ChangeState(ZombieStates newState) 
    {
        activeState = newState;
        
        switch (activeState) 
        {
            case ZombieStates.Follow:
                SetDoMove(true);
                SetDoFollow(true);
                break;
            case ZombieStates.Patrol:
                SetDoMove(true);
                SetDoFollow(false);
                //StopMovement();
                break;
            // case ZombieStates.Move: // если активный шаг == Мув, то включить движение зомби 
            //     SetDoMove(true);
            //     break;
            case ZombieStates.Attack: // если активный шаг == Аттак, то отключить движение зомби 
                SetDoMove(false);
                StopMovement();
                break;
        } 
    }

    public override void Death()
    {
        base.Death();
        CreatePickUp(_medicineChest);
    }

    public void CreatePickUp(MedicineChest _medicineChest)
    {
        if (_medicineChest != null)
            if (Chance())
            {
                {
                    Instantiate(_medicineChest, transform.position, Quaternion.identity);
                }
            }
    }
    
    bool Chance()
    {
        int chance = Random.Range(0, 100);
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
    }
}
