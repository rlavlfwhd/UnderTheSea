using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 2f;
    public float attackDamage = 10f; // 공격력
    public float attackRange = 0.7f; // 공격 범위
    public float attackRate = 6f; // 공격 간격 (공격 딜레이)
    public float roamTime = 3f;
    public LayerMask playerLayer;

    private float nextAttackTime = 2f; // 다음 공격 가능 시간
    private Vector2 roamTarget;
    private Transform player;
    private Rigidbody2D rb;

    private bool isChasing = false;
    private bool isAttacking = false;

    Animator animator;

    public float minX = -9.5f, maxX = 9.5f;
    public float minY = -3.6f, maxY = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        roamTarget = GetRandomPosition();
        StartCoroutine(Roam());
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if(distanceToPlayer < attackRange)
        {
            if(Time.time >= nextAttackTime && !isAttacking)
            {
                StartCoroutine(Tackle());
                nextAttackTime = Time.time + attackRate;
            }
        }
        else if(distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // 공격 쿨타임이 지났고, 플레이어가 범위 내에 있으면 공격 실행
        /*if (Time.time >= nextAttackTime && IsPlayerInRange())
        {
            animator.SetBool("Tackle", true);
            Tackle();
            nextAttackTime = Time.time + attackRate; // 다음 공격 시간 설정
        }*/
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if(isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if(isChasing)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if(distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            if(Vector2.Distance(transform.position, roamTarget) < 0.5f)
            {
                roamTarget = GetRandomPosition();
            }
            MoveTo(roamTarget);
        }
    }

    IEnumerator Roam()
    {
        while (true)
        {
            yield return new WaitForSeconds(roamTime);

            if (isChasing)
            {
                roamTarget = GetRandomPosition();
            }
            
        }
    }

    void MoveTo(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (direction.x > 0) transform.localScale = new Vector2(1, 1);
        else if (direction.x < 0) transform.localScale = new Vector2(-1, 1);
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // 이동을 부드럽게 하기 위해 Lerp 사용 (감속 효과)
        rb.velocity = Vector2.Lerp(rb.velocity, direction * moveSpeed, 0.2f);

        if (direction.x > 0) transform.localScale = new Vector2(1, 1);
        else if (direction.x < 0) transform.localScale = new Vector2(-1, 1);
    }

    /*bool IsPlayerInRange()
    {
        // 공격 범위 내에 플레이어가 있는지 체크
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        return player != null; // 플레이어가 있으면 true 반환
    }*/


    IEnumerator Tackle()
    {
        isAttacking = true;
        animator.SetBool("Tackle", true);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);
        
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

        //animator.SetBool("Tackle", false);
        //isAttacking = false;
    }

    public void EndAttack()
    {
        animator.SetBool("Tackle", false );
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isChasing)
        {
            //rb.velocity = Vector2.zero;
            roamTarget = GetRandomPosition();
        }
    }
}