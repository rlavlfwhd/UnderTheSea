using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage = 10f; // 공격력
    public float attackRange = 1.5f; // 공격 범위
    public float attackRate = 4f; // 공격 간격 (공격 딜레이)
    public LayerMask playerLayer;    

    private float nextAttackTime = 6f; // 다음 공격 가능 시간

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 공격 쿨타임이 지났고, 플레이어가 범위 내에 있으면 공격 실행
        if (Time.time >= nextAttackTime && IsPlayerInRange())
        {           
            animator.SetBool("Tackle", true);
            Tackle();
            nextAttackTime = Time.time + attackRate; // 다음 공격 시간 설정
        }        
    }

    bool IsPlayerInRange()
    {
        // 공격 범위 내에 플레이어가 있는지 체크
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        return player != null; // 플레이어가 있으면 true 반환
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
                Debug.Log($"적이 플레이어를 공격! {attackDamage}의 데미지");
            }
        }
    }
}