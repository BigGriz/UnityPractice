using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    #endregion Setup

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
}
