using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    private float xInput;

    private Animator ani;
    private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //起点位置，方向，长度，碰撞的层级
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        xInput = Input.GetAxisRaw("Horizontal");
        SetRigidbodyFunc();
        SetAnimator();
        SetPlayerRotate();
    }

    /// <summary>
    /// 设置玩家物理属性
    /// </summary>
    private void SetRigidbodyFunc()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        JumpFunc();
    }

    /// <summary>
    /// 起跳
    /// </summary>
    private void JumpFunc()
    {
        if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("jump star");
        }
    }

    /// <summary>
    /// 设置动画状态
    /// </summary>
    private void SetAnimator()
    {
        ani.SetBool("isMoveing", xInput != 0);
    }

    /// <summary>
    /// 设置玩家旋转方向
    /// </summary>
    private void SetPlayerRotate()
    {
        if (xInput>0)
        {
            Debug.Log("右");
            transform.localEulerAngles= new Vector3(0,0,0);
        }
        else if (xInput<0)
        {
            Debug.Log("左");
            transform.localEulerAngles= new Vector3(0,180,0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y-groundCheckDistance));
    }
}