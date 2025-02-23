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
        if (moveX > 0 && !m_FacingRight) //�Է��� ������, �÷��̾ ���� ���� �ִٸ�
        {
            Flip();                                         //�÷��̾ �����´�.
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

    private void Flip()                             //�ø� �Լ��� ȣ�� �Ͽ� ���� ��ȯ
    {
        m_FacingRight = !m_FacingRight;             //�÷��̾ �ٶ󺸴� ������ ��ȯ
        Vector3 theScale = transform.localScale;    //�÷��̾��� x ���� �������� -1�� ���ؼ� ������ �ش�.
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}

