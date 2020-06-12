using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModSlot : MonoBehaviour
{
    // Image Displayed
    private Image image;
    private Sprite defaultSprite;

    [Header("Setup Requirements")]
    public List<AbilityMods> mods;
    public GameObject options;
    public GameObject modPrefab;
    [Header("Active Mod")]
    public AbilityMods activeMod;

    // Start add a Mod Prefab with Ability Mod
    // Set Requirements
    private void Start()
    {
        options.SetActive(false);
        image = GetComponent<Image>();
        defaultSprite = image.sprite;
    }

    private void Awake()
    {
        foreach(AbilityMods n in mods)
        {
            ModOption temp = Instantiate(modPrefab, options.transform).GetComponent<ModOption>();
            temp.mod = n;
            temp.modSlot = this;
        }
    }

    // Left Click Show Options
    public void ToggleOptions()
    {
        options.SetActive(!options.activeSelf);
    }

    public void SetSprite(Sprite _sprite)
    {
        if (_sprite)
        {
            image.sprite = _sprite;
        }
        else
        {
            image.sprite = defaultSprite;
        }
    }

}
