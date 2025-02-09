using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public UnityEngine.UI.Image PlayerHealth; // ü�¹� �̹���
                                              // public Text healthText; // ü�� �ؽ�Ʈ
    public float maxHealth = 225f; // �ִ� ü��
    public float currentHealth = 225f; // ���� ü��

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
        currentHealth = Mathf.Max(0, currentHealth); // ü���� 0 ���Ϸ� �������� �ʵ��� �մϴ�.
    }
}
