using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX, maxX, minY, maxY;
    private bool m_FacingRight = true;
    Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float moveX, moveY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");        

        movement = new Vector2(moveX, moveY).normalized * moveSpeed;
        animator.SetFloat("Speed", movement.sqrMagnitude > 0 ? 1.0f : 0.0f);

        
        //float speed = (moveX != 0 || moveY != 0) ? 1.0f : 0.0f;
        //animator.SetFloat("Speed", speed);
    }

    void FixedUpdate()
    {
        if (moveX > 0 && !m_FacingRight) //입력이 오른쪽, 플레이어가 왼쪽 보고 있다면
        {
            Flip();                                         //플레이어를 뒤집는다.
        }
        else if (moveX < 0 && m_FacingRight)
        {
            Flip();
        }
               
        rb.velocity = movement;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
                
        /*float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector2(clampedX, clampedY);*/
    }

    private void Flip()                             //플립 함수를 호출 하여 방향 전환
    {
        m_FacingRight = !m_FacingRight;             //플레이어가 바라보는 방향을 전환
        Vector3 theScale = transform.localScale;    //플레이어의 x 로컬 스케일을 -1로 곱해서 뒤집어 준다.
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}

