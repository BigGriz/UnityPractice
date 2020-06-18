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
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameEvents.instance.ToggleAOE();
        }

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
    // Check all Keys
    public void GetHotKeys()
    {
        // Get all KeyStates
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            // Check which Pressed this Frame
            if (Input.GetKeyDown(vKey))
            {
                // Toggle Menu if Appropriate
                GameEvents.instance.ToggleMenu(vKey);
            }
        }
    }
}
