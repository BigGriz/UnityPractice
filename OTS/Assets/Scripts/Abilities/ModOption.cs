using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModOption : MonoBehaviour
{
    // Parent Slot
    [HideInInspector] public ModSlot modSlot;
    // Store Mod
    public AbilityMods mod;
    private void Start()
    {
        // Safety Check
        if (!mod)
        {
            Destroy(this.gameObject);
        }
        // Set Sprite to SO
        GetComponent<Image>().sprite = mod.sprite;
    }

    // Set/Unset Parents mod
    public void SetActiveMod()
    {
        if (modSlot.activeMod != mod)
        {
            modSlot.SetSprite(mod.sprite);
            modSlot.activeMod = mod;
        }
        else
        {
            modSlot.SetSprite(null);
            modSlot.activeMod = null;
        }
        GameEvents.instance.GetAbilityMods();
        modSlot.ToggleOptions();
    }
}
