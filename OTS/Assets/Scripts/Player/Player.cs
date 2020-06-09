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
        // Shoot a Projectile
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseAbility();
        }
    }

    // Use Ability
    void UseAbility()
    {
        // Check for Line of Sight
        if (CheckLOS())
        {
            Projectile temp = Instantiate(prefab, transform.position, transform.rotation).GetComponent<Projectile>();
            temp.Seek(targeting.target);
        }
    }

    bool CheckLOS()
    {
        // Grab all Objects between Target & Self
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, targeting.target.transform.position - transform.position, Mathf.Infinity);
        // Check if any are Environmental/Blocking
        for (int i = 0; i < hits.Length; i++)
        {
            // If so Return False
            if (hits[i].collider.gameObject.tag == "Environment")
            {
               return (false);
            }
        }
        // Else have LOS
        return (true);
    }
}
