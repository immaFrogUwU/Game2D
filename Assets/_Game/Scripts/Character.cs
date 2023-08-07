using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private float hp;
    public string currentAnimName;

    public bool isDead => hp <= 0;
    private void Start()
    {
        OnInit();
    }

    //ham khoi tao de chu dong goi khoi tao luc nao cung duoc
    public virtual void OnInit()
    {
        hp = 100;
    }
    //ham huy
    public virtual void OnDespawn()
    {

    }
    protected virtual void OnDeath()
    {

    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }

    }
    public void OnInit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;   //hp = hp - damage
            if (isDead)
            {
                OnDeath();
            }
        }
    }

   
}
