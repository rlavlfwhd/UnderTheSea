using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    private float autoSaveInterval = 1f;    

    void Start()
    {
        LoadPlayerData();
        StartCoroutine(AutoSaveRoutine());
    }

    void LoadPlayerData()
    {
        DPlayer data = GameMaster.Instance.gameData.dPlayer;

        transform.position = data.position;        

        Debug.Log($"시간 {data.playTime}. 체력: {data.currentHealth}");
    }
    
    void Update()
    {
        GameMaster.Instance.gameData.dPlayer.position = transform.position;
        GameMaster.Instance.gameData.dPlayer.playTime = Time.deltaTime;
    }

    void OnApplicationQuit()
    {
        GameMaster.Instance.SaveGameData();
    }

    IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            GameMaster.Instance.SaveGameData();
        }
    }

    public void TakeDamage(float damage)
    {
        DPlayer data = GameMaster.Instance.gameData.dPlayer;
        if (data.currentHealth > 0) data.currentHealth = 0;

        GameMaster.Instance.SaveGameData();

        Debug.Log($"체력 감소: {data.currentHealth}");
    }

    public void Heal(float healAmount)
    {
        DPlayer data = GameMaster.Instance.gameData.dPlayer;

        data.currentHealth += healAmount;
        if (data.currentHealth > data.maxHealth) data.currentHealth = data.maxHealth;

        GameMaster.Instance.SaveGameData();
        
        Debug.Log($"회복: {data.currentHealth}");
    }
}
