using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float speed = 5;
    [SerializeField] private Animator anim;
    [SerializeField] private float jumpForce = 350;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDeath = false;

    private int coin = 0;
    private float horizontal;
    private string currentAnimName;
    //private float vertical; 
    // Start is called before the first frame update
    void Start()
    {
        
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

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
       
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
        }
    }
}
