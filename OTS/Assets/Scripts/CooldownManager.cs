using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    [Header("Ability SO Array")]
    [Tooltip("Ensure names match their respective ability")]
    public AbilityStats[] abilityCooldowns;

    // Sets Cooldowns
    public void SetCooldowns(string _name, float _time)
    {
        foreach (AbilityStats n in abilityCooldowns)
        {
            if (n.name == _name)
            {
                n.StartCooldown(_time);
            }
        }
    }

    private void Update()
    {
        foreach (AbilityStats n in abilityCooldowns)
        {
            n.UpdateTick();
        }
    }
}
