using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public UnityEngine.UI.Image PlayerHealth; // 체력바 이미지
                                              // public Text healthText; // 체력 텍스트
    public float maxHealth = 225f; // 최대 체력
    public float currentHealth = 225f; // 현재 체력

    void Update()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        PlayerHealth.fillAmount = currentHealth / maxHealth;
        //healthText.text = $"{currentHealth}/{maxHealth}";
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // 체력이 0 이하로 감소하지 않도록 합니다.
    }
}
