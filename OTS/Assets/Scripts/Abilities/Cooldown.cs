using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [HideInInspector] public float cooldownTimer = 0;
    [HideInInspector] public float cooldown = 0;
    private Image imageCooldown;

    void Awake()
    {
        imageCooldown = GetComponent<Image>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;       
        imageCooldown.fillAmount = cooldownTimer / cooldown;
    }

    public void Begin(float _timer)
    {
        cooldown = _timer;
        cooldownTimer = cooldown;
    }

    public bool Ready()
    {
        return (cooldownTimer <= 0.0f);
    }
}
