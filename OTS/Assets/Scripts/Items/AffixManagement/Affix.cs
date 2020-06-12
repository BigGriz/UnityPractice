using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AffixType
{
    Suffix = 0,
    Prefix
}

[CreateAssetMenu(fileName = "Affix", menuName = "Item/Affix")]
public class Affix : ScriptableObject
{
    [Header("Affix Type")]
    public AffixType type;
    new public string name;
    [Header("Damage Stats")]
    public int baseDamage;
    public float percentDamage;
    public float atkSpeed;
    public float baseCrit;
    public float percentCrit;
    [Header("Resource Stats")]
    public float energyPerSec;
    public float energyOnKill;
    public float bonusReward;
    public float bonusXP;
    [Header("Abilities")]
    public bool autoUpgrade;
    [Header("Affix Tier")]
    public int tier = 1;
    // List of all Properties
    public List<object> stats;

    public void Setup()
    {
        stats = new List<object>();
        SetupList();
    }

    public void SetupList()
    {
        // Clear List
        stats.Clear();
        // Get all Variables and Store Values in a List
        var fields = this.GetType().GetFields();
        foreach (var n in fields)
        {
            stats.Add(n.GetValue(this));
        }
    }
    public List<object> GetList()
    {
        // Return Values List
        return (stats);
    }
}
