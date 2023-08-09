using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    private IState currentState;
    private bool isRight = true;
    public Character target;

    public Character Target => target;
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
        DeActiveAttack();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);   //neu Destroy(this) chi xoa script khong xoa toan bo
    }
    protected override void OnDeath()
    {
        ChangeState(null);
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
        ChangeAnim("attack");
        rb.velocity = Vector2.zero;
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    public bool IsTargetInRange()
        //trong tam danh, co muc tieu va khoang cach cua bot den player <= khoang co attr thi IsTargetInRange = true
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)  //va cham cung
    {
  
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = !isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.up * 180) : Quaternion.Euler(Vector3.zero);    ///0 0 0 la phai, x180 la trai
    }

    internal void SetTarget(Character character)
    {
        this.target = character;
        //trong tam danh cua bot
        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else
        if (Target != null)
        {
            //khong trong sight thi ve patrol
            ChangeState(new PatrolState());
        }        
        else
        {
            ChangeState(new IdleState());
        } 
    }
    private void ActiveAttack()
    {
        //Ham nay de gay dame cho bot, vi tan cong thuong chua co dame
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }


}
