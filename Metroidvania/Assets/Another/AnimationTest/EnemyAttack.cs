using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage = 10f; // ���ݷ�
    public float attackRange = 1.5f; // ���� ����
    public float attackRate = 4f; // ���� ���� (���� ������)
    public LayerMask playerLayer;    

    private float nextAttackTime = 6f; // ���� ���� ���� �ð�

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ���� ��Ÿ���� ������, �÷��̾ ���� ���� ������ ���� ����
        if (Time.time >= nextAttackTime && IsPlayerInRange())
        {           
            animator.SetBool("Tackle", true);
            Tackle();
            nextAttackTime = Time.time + attackRate; // ���� ���� �ð� ����
        }        
    }

    bool IsPlayerInRange()
    {
        // ���� ���� ���� �÷��̾ �ִ��� üũ
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        return player != null; // �÷��̾ ������ true ��ȯ
    }

    
    void Tackle()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (player != null)
        {
            IDamage damageable = player.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                Debug.Log($"���� �÷��̾ ����! {attackDamage}�� ������");
            }
        }
    }
}