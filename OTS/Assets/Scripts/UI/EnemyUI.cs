using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [Header("Enemy Displayed")]
    public Enemy enemy;
    [Header("UI Elements")]
    public Image enemyPortrait;
    public Image healthPercent;
    public Image manaPercent;

    // Script is not Active Until Target is Found
    private void Update()
    {
        // Safety Check
        if (enemy)
        {
            // Set HealthBars         
            healthPercent.fillAmount = enemy.health / enemy.maxHealth;
            if (enemy.usesMana)
            {
                manaPercent.fillAmount = enemy.mana / enemy.maxMana;
            }
        }
    }

    public void OnUse(Enemy _enemy)
    {
        enemy = _enemy;
        enemyPortrait.sprite = enemy.portrait;
    }
}
