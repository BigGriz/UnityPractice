using System.Reflection;
using System.Linq;
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
    // Actual Stats of the Item
    public List<object> stats;

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
        foreach(Affix n in prefixes)
        {
            n.Setup();
        }
        foreach (Affix n in suffixes)
        {
            n.Setup();
        }
        // Resize List
        stats = new List<object>(typeof(Affix).GetFields().Length);
        CalcStats();
    }

    public void CalcStats()
    { 
        foreach (Affix n in prefixes)
        {
            for (int i = 0; i < n.stats.Count; i++)
            {
                if (stats.Count <= i)
                {
                    stats.Add(n.stats[i]);
                }
                else
                {
                    if (stats[i].GetType() == typeof(int))
                    {
                        stats[i] = (int)stats[i] + (int)n.stats[i];
                    }
                    if (stats[i].GetType() == typeof(float))
                    {
                        stats[i] = (float)stats[i] + (float)n.stats[i];
                    }
                    if (stats[i].GetType() == typeof(bool))
                    {
                        if ((bool)n.stats[i] == true)
                        {
                            stats[i] = true;
                        }
                    }
                    else
                    {
                        // Do Nothing - Seperate into Prefix List in Future
                    }
                }
            }         
        }
        foreach (Affix n in suffixes)
        {
            for (int i = 0; i < n.stats.Count; i++)
            {
                if (stats.Count <= i)
                {
                    stats.Add(n.stats[i]);
                }
                else
                {
                    if (stats[i].GetType() == typeof(int))
                    {
                        stats[i] = (int)stats[i] + (int)n.stats[i];
                    }
                    if (stats[i].GetType() == typeof(float))
                    {
                        stats[i] = (float)stats[i] + (float)n.stats[i];
                        
                    }
                    if (stats[i].GetType() == typeof(bool))
                    {
                        if ((bool)n.stats[i] == true)
                        {
                            stats[i] = true;
                        }
                    }
                    else
                    {
                        // Do Nothing - Seperate into Suffix List in Future
                    }
                }
            }
        }
    }

    public List<object> GetStats()
    {
        return stats;
    }
}
