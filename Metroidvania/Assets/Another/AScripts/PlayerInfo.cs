using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour, IDamage
{
    public Image PlayerHealth;
    private float autoSaveInterval = 1f;    

    void Start()
    {
        LoadPlayerData();
        StartCoroutine(AutoSaveRoutine());
    }

    void LoadPlayerData()
    {
        DPlayer data = GameMaster.Instance.gameData.dPlayer;

        transform.position = data.pPos;

        Debug.Log($"시간 {data.playTime}. 체력: {data.pHP}");
    }
    
    void Update()
    {
        DPlayer data = GameMaster.Instance.gameData.dPlayer;
        PlayerHealth.fillAmount = data.pHP / data.pMHP;

        GameMaster.Instance.gameData.dPlayer.pPos = transform.position;
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
        data.pHP -= damage;
        if (PlayerHealth.fillAmount == 0f)
        {
            Destroy(this.gameObject);
        }


        GameMaster.Instance.SaveGameData();

        Debug.Log($"체력 감소: {data.pHP}");
    }

    public void Heal(float healAmount)
    {
        DPlayer data = GameMaster.Instance.gameData.dPlayer;

        data.pHP += healAmount;
        if (data.pHP > data.pMHP) data.pHP = data.pMHP;

        GameMaster.Instance.SaveGameData();
        
        Debug.Log($"회복: {data.pHP}");
    }
}
