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
}
