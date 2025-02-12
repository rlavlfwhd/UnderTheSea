using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    private int maxEnemies = 5;
    
    void Start()
    {
        LoadEnemies();
        StartCoroutine(SpawnEnemyRoutine());
    }

    void LoadEnemies()
    {
        foreach(DEnemy enemyData in GameMaster.Instance.gameData.enemies)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemyData.ePos, Quaternion.identity);
            enemy.GetComponent<EnemyInfo>().eHP = enemyData.eHP;
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);

            if(GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
