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
        Debug.Log("����");

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

        StartCoroutine(WaitForAnimationStart("isDead")); //  �ִϸ��̼� ���۵� ������ ��ٸ���
    }
    

    IEnumerator Die()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // �ִϸ��̼� ���̸�ŭ ���
        Destroy(gameObject);
    }

    IEnumerator WaitForAnimationStart(string animationName)
    {
        yield return new WaitForEndOfFrame(); //  ù ������ ��� (Ʈ���� ���� ����)

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            Debug.Log($"���� �ִϸ��̼�: {animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)}");
            yield return null; // �ִϸ��̼��� �ٲ� ������ ��� Ȯ��
        }

        Debug.Log("�ִϸ��̼� �����! ���� ���� ����");
        StartCoroutine(Die());  //  �ִϸ��̼� ����� �� ���� ����
    }

    public void DestroyEnemy()
    {
        Debug.Log("DestroyEnemy() ȣ���!"); // ������
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
