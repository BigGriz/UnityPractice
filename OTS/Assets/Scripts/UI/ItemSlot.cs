using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public int id;
    public Item item;
    public EquipSlot slot;

    private void Start()
    {
        if (GetComponentInChildren<Item>())
        {
            item = GetComponentInChildren<Item>();
        }
        // Set Event Callback
        GameEvents.instance.setItem += SetItem;
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.setItem -= SetItem;
    }

    public void SetItem(int _id, GameObject _object)
    {
        if (id == _id)
        {
            if (item != null)
            {
                // Swap into inventory
                Destroy(item.gameObject);
            }

            // Check Correct Slot
            if (_object.GetComponent<Item>().slot == slot)
            {
                Item temp = Instantiate(_object, this.transform).GetComponent<Item>();
                if (temp)
                {
                    item = temp;
                }
            }
        }
    }

    public void SetId(int _id)
    {
        id = _id;
        /*if (item)
        {
            item.id = id;
        }*/
    }
}
