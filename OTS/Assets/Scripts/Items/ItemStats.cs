using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemStats", menuName = "ItemStats", order = 1)]
public class ItemStats : ScriptableObject
{
    public Rarity rarity;
    [Header("Affixes")]
    public List<Affix> prefixes;
    public List<Affix> suffixes;

    // Start is called before the first frame update
    public void Setup(Rarity _rarity)
    {
        prefixes = new List<Affix>();
        suffixes = new List<Affix>();

        if (!AffixMaster.instance)
        {
            Debug.LogError("AffixMaster not Found!");
        }

        rarity = _rarity;
        // Change this to random between max & min of rarity - pre existing mods;
        for (int i = 0; i < (int)rarity; i++)
        {
            // Change to take in type of item
            prefixes.Add(AffixMaster.instance.GetRandomPrefix());
            suffixes.Add(AffixMaster.instance.GetRandomSuffix());
        }
    }
}
