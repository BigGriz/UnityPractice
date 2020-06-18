using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETargeting : MonoBehaviour
{
    int layerMask;
    Projector projector;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Environment");
        projector = GetComponent<Projector>();
        projector.enabled = false;

        GameEvents.instance.toggleAOE += ToggleAOE;
    }
    
    public void ToggleAOE()
    {
        projector.enabled = !projector.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (projector.enabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 newPos = new Vector3(hit.point.x, hit.point.y + 5.0f, hit.point.z);
                transform.position = newPos;
            }
        }
    }
}
