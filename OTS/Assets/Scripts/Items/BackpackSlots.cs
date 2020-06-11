using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSlots : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int id = 50;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<ItemSlot>())
            {
                child.GetComponent<ItemSlot>().SetId(id);
            }
            id++;
        }
    }
}
