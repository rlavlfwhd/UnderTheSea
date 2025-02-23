using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    private Rigidbody2D rb;
    Animator animator;    
    int attackCombo = 0;        
    float comboResetTime = 1f;
    float comboTimer = 0f;
    private static readonly int[] KickDamages = { 15, 15, 30 };

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Kick();
            comboTimer = 0f;
        }


        if(attackCombo > 0 && comboTimer >= comboResetTime)
        {
            ResetAttackCombo();
        }
        comboTimer += Time.deltaTime;

        /*if (comboTimer < comboResetTime)
        {
            comboTimer += Time.deltaTime;
        }
        else
        {
            if (attackCombo > 0)
            {
                ResetAttackCombo();
            }
        }*/
    }

    void Kick()
    {
        animator.SetTrigger($"Kick{attackCombo + 1}");
        DealDamage();
        attackCombo = (attackCombo + 1) % 3;
        rb.velocity = Vector2.zero;
        /*if (attackCombo == 0)
        {
            animator.SetTrigger("Kick1");
        }
        else if (attackCombo == 1)
        {
            animator.SetTrigger("Kick2");
        }
        else if (attackCombo == 2)
        {
            animator.SetTrigger("Kick3");
        }*/
                
        //KickDamage(attackCombo);        
    }

    void DealDamage()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach(var obj in hitObjects)
        {
            obj.GetComponent<IDamage>()?.TakeDamage(KickDamages[attackCombo]);
        }
    }
    /*void KickDamage(int comboIndex)
    {
        int[] kickDamages = { 15, 15, 30 }; // 1Ÿ: 15, 2Ÿ: 15, 3Ÿ: 30
        int damage = kickDamages[comboIndex]; // ���� �޺��� �ش��ϴ� ������ ����

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D obj in hitObjects)
        {
            IDamage damageable = obj.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage); // �ش� ���� �޺��� �������� ����
            }
        }
    }*/


    void ResetAttackCombo()
    {       
        attackCombo = 0;        
    }    
}

