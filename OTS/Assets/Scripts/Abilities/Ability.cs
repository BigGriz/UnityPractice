using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [HideInInspector] public PlayerTargeting targeting;
    private Cooldown cooldownUI;

    [Header("Setup Fields")]
    public Sprite abilitySprite;
    public GameObject projectilePrefab;
    [Header("Ability Stats")]
    public float cooldown;

    #region Setup
    private void Awake()
    {
        cooldownUI = GetComponentInChildren<Cooldown>();
        GetComponent<Image>().sprite = abilitySprite;
    }
    private void Start()
    {
        targeting = Player.instance.targeting;
    }
    #endregion Setup

    // Use Ability
    public void UseAbility()
    {
        // Check if Target & Off Cooldown
        if (cooldownUI.Ready() && targeting.target)
        {
            // Check for Line of Sight
            if (CheckLOS())
            {
                cooldownUI.Begin(cooldown);
                Projectile temp = Instantiate(projectilePrefab, Player.instance.transform.position, Player.instance.transform.rotation).GetComponent<Projectile>();
                temp.Seek(targeting.target);
            }
        }
        // Still on CD
        else if (!cooldownUI.Ready())
        {
            Debug.Log("On Cooldown");
        }
        // No Target
        else
        {
            Debug.Log("No Target");
        }
    }

    // Ensure LOS to Target
    bool CheckLOS()
    {
        // Grab all Objects between Target & Self
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Player.instance.transform.position, targeting.target.transform.position - Player.instance.transform.position, Mathf.Infinity);
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
