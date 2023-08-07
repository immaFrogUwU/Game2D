using System.Collections;
using System.Collections.Generic;
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

    public void OnExit(Enemy enemy)
    {
    }
}

    
