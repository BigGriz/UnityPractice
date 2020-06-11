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

    [Header("Dummy Movement")]
    private bool dirRight = true;
    public float speed = 10.0f;
    public CharacterController controller;
    public float timer = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        health = maxHealth;
        mana = maxMana;
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if (dirRight)
        {
            controller.Move(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            controller.Move(-Vector3.right * Time.deltaTime * speed);
        }

        if (timer <= 0.0f)
        {
            timer = 3.0f;
            dirRight = !dirRight;
        }
    }
    public void TakeDamage(float _damage)
    {
        health -= _damage;
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
