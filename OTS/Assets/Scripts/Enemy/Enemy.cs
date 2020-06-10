using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("For LoS Checks")]
    public bool isVisible;

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
