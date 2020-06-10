using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [HideInInspector] public int id;
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
        // Set Event Callback
        GameEvents.instance.useAbility += UseAbility;
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.useAbility -= UseAbility;
    }
    #endregion Setup

    // Use Ability
    public void UseAbility(int _id, GameObject _target)
    {
        if (_id == this.id)
        {
            // Check if Target & Off Cooldown
            if (cooldownUI.Ready() && _target)
            {
                // Check for Line of Sight
                if (CheckLOS(_target))
                {
                    cooldownUI.Begin(cooldown);
                    Projectile temp = Instantiate(projectilePrefab, Player.instance.transform.position, Player.instance.transform.rotation).GetComponent<Projectile>();
                    temp.Seek(_target);
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
    }

    // Ensure LOS to Target
    bool CheckLOS(GameObject _target)
    {
        // Grab all Objects between Target & Self
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Player.instance.transform.position, _target.transform.position - Player.instance.transform.position, Mathf.Infinity);
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
