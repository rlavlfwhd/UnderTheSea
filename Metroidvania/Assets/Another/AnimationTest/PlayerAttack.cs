using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;    
    int attackCombo = 0;
        
    float comboResetTime = 1f;
    float comboTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
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

    void Attack()
    {        
        if (attackCombo == 0)
        {
            animator.SetTrigger("Attack1");
        }
        else if (attackCombo == 1)
        {
            animator.SetTrigger("Attack2");
        }
        else if (attackCombo == 2)
        {
            animator.SetTrigger("Attack3");
        }
        
        attackCombo = (attackCombo + 1) % 3;
    }

    void ResetAttackCombo()
    {       
        attackCombo = 0;
        animator.SetTrigger("Idle");
    }
}

