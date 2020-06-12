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
    public AbilitySlot mouseOverAbility;
    public ItemSlot mouseOverEquipment;
    public ItemSlot mouseOverBackpack;

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
        if (setAbility != null && mouseOverAbility != null)
        {
            setAbility(mouseOverAbility.id, _ability);
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

    // Toggle Menu w/ HotKeys
    public event Action<KeyCode> toggleMenu;
    public void ToggleMenu(KeyCode _key)
    {
        if (toggleMenu != null)
        {
            toggleMenu(_key);
        }
    }

    // Item Dragged to Inventory
    public event Action<int, GameObject> setItem;
    public void SetItem(GameObject _item)
    {
        if (setItem != null && mouseOverEquipment != null)
        {
            setItem(mouseOverEquipment.id, _item);
        }
    }

    // Item Dragged to Inventory
    public event Action<int, GameObject> swapItem;
    public void SwapItem(GameObject _item)
    {
        if (swapItem != null && mouseOverBackpack != null)
        {
            swapItem(mouseOverBackpack.id, _item);
        }
    }

    // Switches Items
    public event Action<int, GameObject> switchItem;
    public void SwitchItem(int _id, GameObject _item)
    {
        if (switchItem != null)
        {
            switchItem(_id, _item);
        }
    }

    public event Action getAbilityMods;
    public void GetAbilityMods()
    {
        if (getAbilityMods != null)
        {
            getAbilityMods();
        }
    }

}
