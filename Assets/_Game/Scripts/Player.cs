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
    private bool isGrounded;
    private bool isJumping;
    private bool isAttack;
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
        //hori = -1 khi sang trái, =0 im, = 1 sang phải
        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log(CheckGrounded());
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpForce * Vector2.up);
        }
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            //Neu di sang phai thi khong quay, sang trai thi doi huong player
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
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

    }
    private void Throw()
    {

    }

    private void Jump()
    {
        
    }

    private void ChangeAnim(string animName)
    {
        anim.ResetTrigger(animName);
        currentAnimName = animName;
        anim.SetTrigger(currentAnimName);
    }
}
