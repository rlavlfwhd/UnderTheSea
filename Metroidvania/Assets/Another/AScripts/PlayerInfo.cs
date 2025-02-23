using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour, IDamage
{
    public Image PlayerHealth;
    private float autoSaveInterval = 1f;
    
    private DPlayer data;

    void Start()
    {
        data = GameMaster.Instance.gameData.dPlayer;
        transform.position = data.pPos;
        StartCoroutine(AutoSaveRoutine());
    }        
    
    void Update()
    {        
        PlayerHealth.fillAmount = data.pHP / data.pMHP;

        data.pPos = transform.position;
        data.playTime += Time.deltaTime;
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
        data.pHP -= damage;
        if (PlayerHealth.fillAmount == 0)
        {
            Destroy(this.gameObject);
        }        

        Debug.Log($"체력 감소: {data.pHP}");
    }

    public void Heal(float healAmount)
    {
        data.pHP = Mathf.Min(data.pHP + healAmount, data.pMHP);
        
        Debug.Log($"회복: {data.pHP}");
    }
}
