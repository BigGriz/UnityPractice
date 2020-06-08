using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHighlight : MonoBehaviour
{
    // Setup Variables
    private Vector3 offset;
    private MeshRenderer meshRenderer;
    private PlayerTargeting player;
    [HideInInspector] public GameObject target;

    private void Start()
    {
        // Singletons
        if (PlayerTargeting.instance != null)
        {
            player = PlayerTargeting.instance;
        }
        else
        {
            Debug.Log("No Player Targeting Instance found in Target Highlighter");
        }
        // Setup
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Check if Target still in Range
        if (CheckDistance())
        {
            Toggle(true);
            transform.position = target.transform.position + offset;
        }
        // Else release Target
        else
        {
            target = null;
            Toggle(false);
        }
    }

    // Set Parent & Get Offset
    public void NewTarget(GameObject _new)
    {
        offset = new Vector3(0.0f, _new.GetComponent<Collider>().bounds.extents.y * 2.0f, 0.0f);
        target = _new;
        transform.parent = _new.transform;
    }
    // Toggle Renderer
    public void Toggle(bool _on)
    {
        meshRenderer.enabled = _on;
    }
    // Check Distance
    public bool CheckDistance()
    {
        if (target)
        {
            float distance = Vector3.Distance(player.gameObject.transform.position, target.transform.position);
            distance -= target.GetComponent<Collider>().bounds.extents.x;
            return (distance < player.targetRange);
        }
        return false;
    }
}
