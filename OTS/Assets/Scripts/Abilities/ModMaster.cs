using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModMaster : MonoBehaviour
{
    [Header("Ability SO Array")]
    [Tooltip("Ensure names match their respective ability")]
    public AbilityStats[] abilities;

    // Cleanup Mods
    public void Start()
    {
        foreach (AbilityStats n in abilities)
        {
            n.Setup();
        }
    }

    // Sets Cooldowns
    public void SetCooldowns(string _name, float _time)
    {
        foreach (AbilityStats n in abilities)
        {
            if (n.name == _name)
            {
                n.StartCooldown(_time);
            }
        }
    }

    private void Update()
    {
        foreach (AbilityStats n in abilities)
        {
            n.UpdateTick();
        }
    }
}
