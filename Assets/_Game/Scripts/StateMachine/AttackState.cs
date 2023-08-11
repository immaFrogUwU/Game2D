using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    //dau tien
    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x < enemy.transform.position.x);
            //enemy.StopMoving();
            enemy.Attack();
        }
        
        timer = 0;
    }
    //update lien tuc
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
        
    }
    //chuyen state khac thi exit state cu va OnEnter state moi
    public void OnExit(Enemy enemy)
    {
        
    }
}