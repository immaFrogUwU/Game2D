using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float speed = 5;

    [SerializeField] private float jumpForce = 350;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDeath = false;

    private int coin = 0;
    private float horizontal;
    

    private Vector3 savePoint;     
    //private float vertical;
    // Start is called before the first frame update
    void Start()
    {
        //first pos of player
        SavePoint();
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        //hori = -1 khi sang trái, =0 im, = 1 sang phải
        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log(CheckGrounded());

        if (isDeath)
        {
            return;
        }
        if (isAttack)
        {
            return;
        }
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                return;
            }

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
            //Attack
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
                return;
            }

            //Throw
            if (Input.GetKeyDown(KeyCode.V))
            {
                Throw();
                return;
            }

        }

        //Check falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }

        //Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            //Neu di sang phai thi khong quay, sang trai thi doi huong player
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        //idle
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }
    //Reset cac thong so ve Start
    public override void OnInit()
    {
        base.OnInit();
        isDeath = false;
        isAttack = false;
        isJumping = false;
        transform.position = savePoint;
        ChangeAnim("idle");
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayerMask);
        
        //if (hit.collider != null)                    //va chạm với collider
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        return hit.collider != null;
    }
    
    private void Attack()
    {
        ChangeAnim("attack");
        isAttack = true;
        rb.velocity = Vector2.zero; /// khóa lại vận tốc
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void Throw()
    {
        ChangeAnim("throw");
        isAttack = true;
        rb.velocity = Vector2.zero; /// khóa lại vận tốc
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void ResetAttack()
    {
        isAttack = false;   
        ChangeAnim("idle");
    }

    private void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            isDeath = true;
            ChangeAnim("die");
            Invoke(nameof(OnInit), 1f);
        }
    }

    
}
