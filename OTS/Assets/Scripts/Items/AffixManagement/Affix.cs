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
}
