using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [HideInInspector] public PlayerTargeting targeting;
    [HideInInspector] public PlayerMovement movement;
    
    [Header("Player Stats")]
    public float movementSpeed;
    public float range;
    [HideInInspector] public float health;
    public float maxHealth;
    [HideInInspector] public float mana;
    public float maxMana;


    public GameObject prefab;

    #region Singleton
    private void Awake()
    {
        // Singleton
        if (instance != null)
        {
            Debug.LogError("Player Instance Exists!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        targeting = GetComponent<PlayerTargeting>();
        movement = GetComponent<PlayerMovement>();
        health = maxHealth;
        mana = maxMana;
    }
    #endregion Singleton

    void Update()
    {
        // Check for Abilities
        GetAbilityKeys();
        // Check for Hotkeys
        GetHotKeys();
    }

    // Spend Mana for Spell
    public void SpendMana(float _cost)
    {
        mana -= _cost;
    }

    // Check Num Keys
    public void GetAbilityKeys()
    {
        for (int number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
            {
                // Use Ability
                GameEvents.instance.UseAbility(number);
                return;
            }
        }
        return;
    }

    public void GetHotKeys()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Use Ability
            GameEvents.instance.ToggleSpellBook();
        }
    }
}
