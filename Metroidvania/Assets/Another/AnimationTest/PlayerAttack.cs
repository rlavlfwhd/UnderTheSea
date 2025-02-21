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

        if (comboTimer < comboResetTime)
        {
            comboTimer += Time.deltaTime;
        }
        else
        {
            if (attackCombo > 0)
            {
                ResetAttackCombo();
            }
        }
    }

    void Kick()
    {
        if (attackCombo == 0)
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
        }
        
        attackCombo = (attackCombo + 1) % 3;
        KickDamage(attackCombo);
        rb.velocity = Vector2.zero;
    }

    void KickDamage(int comboIndex)
    {
        int[] kickDamages = { 15, 15, 30 }; // 1타: 15, 2타: 15, 3타: 30
        int damage = kickDamages[comboIndex]; // 현재 콤보에 해당하는 데미지 적용

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D obj in hitObjects)
        {
            IDamage damageable = obj.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage); // 해당 공격 콤보의 데미지를 적용
            }
        }
    }


    void ResetAttackCombo()
    {       
        attackCombo = 0;        
    }    
}

