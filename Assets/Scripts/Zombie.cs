using UnityEngine;

public class Zombie : EnemyMovement
{
    [Header("Attack config")]
    public float attackRate;

    private float nextAttack;
    
    enum ZombieStates
    {
        Stand,
        Move,
        Attack
    }

    private ZombieStates activeState;

    // Start is called before the first frame update
    public override void StartAdditional()
    {
        ChangeState(ZombieStates.Stand); // состояние зомби при старте игры stand(стоит на месте)
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
                case ZombieStates.Stand: // если activeState == ZombieStates.Stand , зомби стоит на месте И
                    if (distance <= followDistance) // если дистанция от зомби до игрока меньше followDistance, то активировать движение у зомби
                    {
                        ChangeState(ZombieStates.Move);
                    }

                    break;
                case ZombieStates.Move: // если activeState == ZombieStates.Move , зомби движется в направлении игрока (Rotate()) И
                    if (distance <= attackDistance) // если дистанция от зомби до игрока меньше или равна attackDistance, то активировать атаку у зомби 
                    {
                        ChangeState(ZombieStates.Attack);
                    }
                    else if (distance >= loseDistance) // иначе если дистанция от зомби до игрока больше или равна loseDistance зомби стоит на месте, игрок сбежал
                    {
                        ChangeState(ZombieStates.Stand);
                    }
                    Rotate();
                    break;
                case ZombieStates.Attack: // если activeState == ZombieStates.Attack , зомби движется в направлении игрока (Rotate()) И
                    if (distance > attackDistance) // если дистанция от зомби до игрока больше attackDistance, то активировать Движение у зомби 
                    {
                        ChangeState(ZombieStates.Move);
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
            case ZombieStates.Stand: // если активный шаг == Стэнд, то отключить движение зомби 
                SetDoMove(false);
                StopMovement();
                break;
            case ZombieStates.Move: // если активный шаг == Мув, то включить движение зомби 
                SetDoMove(true);
                break;
            case ZombieStates.Attack: // если активный шаг == Аттак, то отключить движение зомби 
                SetDoMove(false);
                StopMovement();
                break;
        } 
    }

    private void OnDrawGizmos()
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
