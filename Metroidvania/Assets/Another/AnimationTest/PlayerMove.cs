using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minY = -2f;
    public float maxY = 2f;

    private bool m_FacingRight = true;
    Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float moveX;

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
        float moveY = Input.GetAxisRaw("Vertical");        

        movement = new Vector2(moveX, moveY).normalized * moveSpeed;

        float speed = (moveX != 0 || moveY != 0) ? 1.0f : 0.0f;
        animator.SetFloat("Speed", speed);
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
        // �̵� ����
        rb.velocity = new Vector2(movement.x, movement.y);

        // Y�� �̵� ���� ����
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector2(transform.position.x, clampedY);
    }

    private void Flip()                             //�ø� �Լ��� ȣ�� �Ͽ� ���� ��ȯ
    {
        m_FacingRight = !m_FacingRight;             //�÷��̾ �ٶ󺸴� ������ ��ȯ
        Vector3 theScale = transform.localScale;    //�÷��̾��� x ���� �������� -1�� ���ؼ� ������ �ش�.
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}

