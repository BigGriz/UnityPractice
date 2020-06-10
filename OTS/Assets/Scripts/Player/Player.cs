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
    }
    #endregion Singleton

    void Update()
    {
        // Check for Keys
        GetAbilityKeys();
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
}
