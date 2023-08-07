using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    private IState currentState;
    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }
    public override void OnDespawn()
    {
        base.OnDespawn(); 
    }
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    //khi doi sang state moi se check xem state cu co = null khong
    
    public void ChangeState(IState newState)
    {
        if (currentState != null)     //khac null doi sang state moi
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState?.OnEnter(this);      //khac null thi truy cap state moi do
    }
    public void Moving()
    {
        ChangeAnim("run");
        //huong ra phia truoc mat enemy
        rb.velocity = transform.right * moveSpeed;

    }
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        
    }
    public bool IsTargetInRange()
    {
        return false;
    }
}
