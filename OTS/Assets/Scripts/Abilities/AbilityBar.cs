using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int id = 1;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<AbilitySlot>())
            {
                child.GetComponent<AbilitySlot>().SetId(id);
            }
            id++;
        }
    }
}
