using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellStats", menuName = "SpellStats", order = 1)]
public class AbilityStats : ScriptableObject
{
    public float cooldown;
    new public string name;
    public List<AbilityMods> mods;

    // Update is called once per frame
    public void UpdateTick()
    {
        cooldown -= Time.deltaTime;
    }

    public void StartCooldown(float _time)
    {
        cooldown = _time;
    }

    public void Setup()
    {
        mods.Clear();
        cooldown = 0.0f;
    }  
}
