using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common = 0,
    Uncommon,
    Rare,
    Epic
}

[CreateAssetMenu(fileName = "ItemStats", menuName = "ItemStats", order = 1)]
public class ItemStats : ScriptableObject
{
    [Header("Attributes")]
    public Rarity rarity;
    public EquipSlot slot;
    public int tier;
    [Header("Affixes")]
    public List<Affix> prefixes;
    public List<Affix> suffixes;

    // Start is called before the first frame update
    public void Setup(EquipSlot _slot, int _tier)
    {
        // No white items for testing
        rarity = (Rarity)Random.Range(1, Rarity.GetNames(typeof(Rarity)).Length);
        slot = _slot;
        tier = _tier;
        prefixes = new List<Affix>();
        suffixes = new List<Affix>();
        
        // Change this to random between max & min of rarity - pre existing mods;
        for (int i = 0; i < (int)rarity; i++)
        {
            // Change to take in type of item
            prefixes.Add(AffixMaster.instance.GetRandomPrefix());
            suffixes.Add(AffixMaster.instance.GetRandomSuffix());
        }
    }
}
