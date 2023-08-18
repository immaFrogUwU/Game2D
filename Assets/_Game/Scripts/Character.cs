using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;
    public float hp;
    public string currentAnimName;

    public bool isDead => hp <= 0.1f;
    private void Start()
    {
        OnInit();
    }

    //ham khoi tao de chu dong goi khoi tao luc nao cung duoc
    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100, transform);
    }
    //ham huy
    public virtual void OnDespawn()
    {
        OnInit();
    }
    protected virtual void OnDeath()
    {
           ChangeAnim("die");
           Invoke(nameof(OnDespawn), 1f);
        
    }
    internal virtual void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }

    }
    public void OnHit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;   //hp = hp - damage
            if (isDead)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHp(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
            //1 prefab, 2 vi tri, 3 goc xoay identity mac dinh la goc xoay 0 0 0
        }
    }
    
   
}
