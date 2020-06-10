using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("For LoS Checks")]
    public bool isVisible;
    [Header("Stats")]
    public float health;
    public float maxHealth;
    public bool usesMana;
    public float mana;
    public float maxMana;
    [Header("UI Elements")]
    public Sprite portrait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(float _damage)
    {

    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
