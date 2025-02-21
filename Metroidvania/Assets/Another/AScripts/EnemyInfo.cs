using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour, IDamage
{
    public float eMHP = 50f;
    public float eHP;
    
    void Start()
    {
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
        Debug.Log("¹ÞÀ½");

        if(eHP <= 0)
        {
            Destroy(this.gameObject);
        }

        SaveEnemyData();
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
