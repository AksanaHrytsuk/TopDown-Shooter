using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyMovement
{

    enum ShooterStates
    {
        Stand,
        Move,
        Attack
    }

    private ShooterStates activeState;

    // Start is called before the first frame update
    public override void StartAdditional()
    {
        ChangeState(ShooterStates.Stand); // состояние зомби при старте игры stand(стоит на месте)
    }

    public override void FUpdate()
    {
        UpdateState();
    }

    void UpdateState()
    {
        if (GetPlayer() != null)
        {
            float distance = Vector3.Distance(transform.position, GetPlayer().transform.position); // дистанция от зомби к игроку


            switch (activeState)
            {
                case ShooterStates.Stand: // если activeState == ShooterStates.Stand , зомби стоит на месте И
                    if (distance <= followDistance) // если дистанция от зомби до игрока меньше followDistance, то активировать движение у зомби
                    {
                        ChangeState(ShooterStates.Move);
                    }

                    break;
                case ShooterStates.Move: // если activeState == ShooterStates.Move , зомби движется в направлении игрока (Rotate()) И
                    if (distance <= attackDistance) // если дистанция от зомби до игрока меньше или равна attackDistance, то активировать атаку у зомби 
                    {
                        ChangeState(ShooterStates.Attack);
                    }
                    else if (distance >= loseDistance) // иначе если дистанция от зомби до игрока больше или равна loseDistance зомби стоит на месте, игрок сбежал
                    {
                        ChangeState(ShooterStates.Stand);
                    }
                    Rotate();
                    break;
                case ShooterStates.Attack: // если activeState == ShooterStates.Attack , зомби движется в направлении игрока (Rotate()) И
                    if (distance > attackDistance) // если дистанция от зомби до игрока больше attackDistance, то активировать Движение у зомби 
                    {
                        ChangeState(ShooterStates.Move);
                    }
                    Rotate();
                    break;
            }
        } 
    }

    void ChangeState(ShooterStates newState) 
    {
        activeState = newState;
        
        switch (activeState) 
        {
            case ShooterStates.Stand: // если активный шаг == Стэнд, то отключить движение зомби 
                SetDoMove(false);
                GetWeapon().SetCanShoot(false);
                StopMovement();
                break;
            case ShooterStates.Move: // если активный шаг == Мув, то включить движение зомби 
                SetDoMove(true);
                GetWeapon().SetCanShoot(true);
                break;
            case ShooterStates.Attack: // если активный шаг == Аттак, то отключить движение зомби 
                SetDoMove(false);
                GetWeapon().SetCanShoot(true);
                StopMovement();
                break;
        } 
    }
    
    public override void Death()
    {
        base.Death();
        Destroy(GetWeapon());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, followDistance); 
        
        Gizmos.DrawWireSphere(position, attackDistance);
        Gizmos.color = Color.red;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(position, loseDistance);
    }
}
