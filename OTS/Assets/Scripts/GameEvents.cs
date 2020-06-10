using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvents : MonoBehaviour
{
    #region Singleton
    public static GameEvents instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Game Events System Exists!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion Singleton

    #region Setup
    private Player player;
    private CooldownManager cooldownManager;
    private void Start()
    {
        if (!Player.instance)
        {
            Debug.LogError("No Player Instance Exists!");
        }
        else
        {
            player = Player.instance;
        }

        cooldownManager = GetComponent<CooldownManager>();
    }
    #endregion Setup
    [Header("Slot Moused Over")]
    public AbilitySlot mouseOver;

    // Enemy is Targeted
    public event Action<Enemy> onGetTarget;
    public void OnGetTarget(Enemy _enemy)
    {
        if (onGetTarget != null)
        {
            onGetTarget(_enemy);
        }
    }
    // Enemy is not Targeted
    public event Action onLoseTarget;
    public void OnLoseTarget()
    {
        if (onLoseTarget != null)
        {
            onLoseTarget();
        }
    }
    // Ability Key is Pressed
    public event Action<int, GameObject> useAbility;
    public void UseAbility(int _id)
    {
        if (useAbility != null)
        {
            useAbility(_id, player.targeting.target);
        }
    }
    // Sets Cooldowns
    public void SetCooldowns(string _name, float _time)
    {
        cooldownManager.SetCooldowns(_name, _time);
    }
    // Ability Dragged to Bar
    public event Action<int, GameObject> setAbility;
    public void SetAbility(GameObject _ability)
    {
        if (setAbility != null && mouseOver != null)
        {
            setAbility(mouseOver.id, _ability);
        }
    }
    // Ability Dragged off Bar
    public event Action<int> clearAbility;
    public void ClearAbility(int _id)
    {
        if (clearAbility != null)
        {
            clearAbility(_id);
        }
    }
    // Change this to all Menus?
    public event Action toggleSpellBook;
    public void ToggleSpellBook()
    {
        if (toggleSpellBook != null)
        {
            toggleSpellBook();
        }
    }
    public event Action closeSpellBook;
    public void CloseSpellBook()
    {
        if (closeSpellBook != null)
        {
            closeSpellBook();
        }
    }

}
