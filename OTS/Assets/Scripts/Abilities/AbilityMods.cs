using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityMod", menuName = "AbilityMod", order = 1)]
public class AbilityMods : ScriptableObject
{
    public int numChains;
    public float chainRange;
    public bool explodes;
    public float explosionRadius;
    public Sprite sprite;
}
