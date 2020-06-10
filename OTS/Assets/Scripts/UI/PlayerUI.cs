using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Player player;
    [Header("UI Elements")]
    public Image healthPercent;
    public Image manaPercent;

    private void Start()
    {
        player = Player.instance;
    }

    // Script is not Active Until Target is Found
    private void Update()
    {
        // Safety Check
        if (player)
        {
            // Set HealthBars         
            healthPercent.fillAmount = player.health / player.maxHealth;
            manaPercent.fillAmount = player.mana / player.maxMana;           
        }
    }
}
