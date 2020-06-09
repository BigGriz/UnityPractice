using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [HideInInspector] public PlayerTargeting targeting;
    [HideInInspector] public PlayerMovement movement;

    public AbilityBar abilityBar;

    [Header("Player Stats")]
    public float movementSpeed;
    public float range;
    public GameObject prefab;


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
    }

    void Update()
    {
        // Check for Keys
        GetAbilityKeys();
    }

    // Use Ability
    void UseAbility(int _slot)
    {
        abilityBar.actionBar[_slot].GetComponent<Ability>().UseAbility();
    }

    // Check Num Keys
    public void GetAbilityKeys()
    {
        for (int number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
            {
                UseAbility(number - 1);
                return;
            }
        }

        return;
    }
}
