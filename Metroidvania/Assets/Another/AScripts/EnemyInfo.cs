using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour, IDamage
{
    Animator animator;
    public float eMHP = 50f;
    public float eHP;

    public bool isDead = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();

        LoadEnemyData();
    }

    void LoadEnemyData()
    {
        foreach(DEnemy enemyData in GameMaster.Instance.gameData.enemies)
        {
            if(Vector2.Distance(enemyData.ePos, (Vector2)transform.position) < 0.5f)
            {
                eHP = enemyData.eHP;
                transform.position = enemyData.ePos;
                return;
            }
        }

        eHP = eMHP;
    }


    public void TakeDamage(float damage)
    {
        eHP -= damage;
        Debug.Log("받음");

        if(eHP <= 0)
        {
            StartDestroySequence();
        }

        SaveEnemyData();
    }

    void StartDestroySequence()
    {
        isDead = true;
        animator.SetTrigger("isDead");        

        StartCoroutine(WaitForAnimationStart("isDead")); //  애니메이션 시작될 때까지 기다리기
    }
    

    IEnumerator Die()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // 애니메이션 길이만큼 대기
        Destroy(gameObject);
    }

    IEnumerator WaitForAnimationStart(string animationName)
    {
        yield return new WaitForEndOfFrame(); //  첫 프레임 대기 (트리거 적용 보장)

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            Debug.Log($"현재 애니메이션: {animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)}");
            yield return null; // 애니메이션이 바뀔 때까지 계속 확인
        }

        Debug.Log("애니메이션 실행됨! 이제 삭제 시작");
        StartCoroutine(Die());  //  애니메이션 실행된 후 삭제 시작
    }

    public void DestroyEnemy()
    {
        Debug.Log("DestroyEnemy() 호출됨!"); // 디버깅용
        Destroy(gameObject);
    }


    void SaveEnemyData()
    {
        foreach(DEnemy enemyData in GameMaster.Instance.gameData.enemies)
        {
            if (Vector2.Distance(enemyData.ePos, (Vector2)transform.position) < 0.5f)
            {
                enemyData.eHP = eHP;
                enemyData.ePos = transform.position;
                GameMaster.Instance.SaveGameData();
                return;
            }
        }

        GameMaster.Instance.gameData.enemies.Add(new DEnemy(transform.position, eHP));
        GameMaster.Instance.SaveGameData();
    }    
}
