using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PatrolState : IState
{
    float randomTime;
    float timer = 0;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.Target !=  null)    //co targer
        {
            //doi huong enemy ve huong co player
            enemy.ChangeDirection(enemy.Target.transform.position.x < enemy.transform.position.x);
            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
        }
        else
        {
            if (timer < randomTime)
            {
                //timer be hon thoi gian random thi tiep tuc di chuyen
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
        
       
    }

    public void OnExit(Enemy enemy)
    {
    }
}

    
