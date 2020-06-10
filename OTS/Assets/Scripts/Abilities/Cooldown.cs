using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [HideInInspector] public float cooldownTimer = 0;
    [HideInInspector] public float cooldown = 0;
    private Image imageCooldown;
    public bool ready = false;

    void Awake()
    {
        imageCooldown = GetComponent<Image>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;       
        imageCooldown.fillAmount = cooldownTimer / cooldown;

        if (cooldownTimer <= 0.0f)
        {
            ready = true;
        }
    }

    public void Begin(float _timer)
    {
        ready = false;
        cooldown = _timer;
        cooldownTimer = cooldown;
    }

    public bool Ready()
    {
        return (ready);
    }
}
