using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    public EnemyUI enemyUI;

    #region Setup & Cleanup
    private void Start()
    {
        // Set Event Callbacks
        GameEvents.instance.onGetTarget += OnGetTarget;
        GameEvents.instance.onLoseTarget += OnLoseTarget;

        // Set as Off
        enemyUI.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.onGetTarget -= OnGetTarget;
        GameEvents.instance.onLoseTarget -= OnLoseTarget;
    }
    #endregion Setup & Cleanup

    // Turn on UI Panel
    public void OnGetTarget(Enemy _enemy)
    {
        enemyUI.OnUse(_enemy);
        enemyUI.gameObject.SetActive(true);
    }
    // Turn off UI Panel
    public void OnLoseTarget()
    {
        enemyUI.gameObject.SetActive(false);
    }
}
