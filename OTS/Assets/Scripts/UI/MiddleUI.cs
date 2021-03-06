﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleUI : MonoBehaviour
{
    // Change this to a list of ids in future
    public GameObject spellBook;
    public GameObject character;
    public GameObject backpack;

    #region Setup
    private void Start()
    {
        // UI Elements
        spellBook.SetActive(false);
        character.SetActive(false);
        backpack.SetActive(false);
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
        if (_key == KeyCode.B)
        {
            backpack.SetActive(!backpack.activeSelf);
        }
    }
    // Buttons
    public void ToggleMenuButton(GameObject _menu)
    {
        _menu.SetActive(!_menu.activeSelf);
    }
}
