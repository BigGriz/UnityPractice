using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleUI : MonoBehaviour
{
    // Change this to all Tabs in Future
    public GameObject spellBook;

    #region Setup
    private void Start()
    {
        // UI Elements
        spellBook.SetActive(false);
        // Setup Callbacks
        GameEvents.instance.toggleSpellBook += ToggleSpellBook;
        GameEvents.instance.closeSpellBook += CloseSpellBook;
    }

    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.toggleSpellBook -= ToggleSpellBook;
        GameEvents.instance.closeSpellBook -= CloseSpellBook;
    }
    #endregion Setup

    // Toggle UI Element in Future
    public void ToggleSpellBook()
    {
        spellBook.SetActive(!spellBook.activeSelf);
    }

    public void CloseSpellBook()
    {
        spellBook.SetActive(false);
    }
}
