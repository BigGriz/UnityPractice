using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleUI : MonoBehaviour
{
    // Change this to all Tabs in Future
    public GameObject spellBook;
    public GameObject character;

    #region Setup
    private void Start()
    {
        // UI Elements
        spellBook.SetActive(false);
        character.SetActive(false);
        // Setup Callbacks
        GameEvents.instance.toggleMenu += ToggleMenu;
    }

    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.toggleMenu -= ToggleMenu;
    }
    #endregion Setup

    // Toggle UI Element in Future
    public void ToggleMenu(KeyCode _key)
    {
        if (_key == KeyCode.P)
        {
            spellBook.SetActive(!spellBook.activeSelf);
        }
        if (_key == KeyCode.C)
        {
            character.SetActive(!character.activeSelf);
        }
    }
    // Buttons
    public void ToggleMenuButton(GameObject _menu)
    {
        _menu.SetActive(!_menu.activeSelf);
    }
}
