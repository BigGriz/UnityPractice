using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public int id;
    public Item item;
    public EquipSlot slot;
    public bool equipment;

    private void Start()
    {
        if (GetComponentInChildren<Item>())
        {
            item = GetComponentInChildren<Item>();
        }
        // Set Event Callback
        GameEvents.instance.setItem += SetItem;
        GameEvents.instance.swapItem += SetItem;
        GameEvents.instance.switchItem += SetItem;
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.setItem -= SetItem;
        GameEvents.instance.swapItem -= SetItem;
        GameEvents.instance.switchItem -= SetItem;
    }

    public void ResetLocals(params Item[] items)
    {
        foreach (Item n in items)
        {
            n.transform.localPosition = Vector3.zero;
            n.transform.localScale = Vector3.one;
        }
    }

    public void SetItem(int _id, GameObject _object)
    {
        if (id == _id)
        {
            // Get Parent
            ItemSlot parent = _object.transform.parent.GetComponent<ItemSlot>();
            Item newItem = _object.GetComponent<Item>();
            
            // Empty Equipment Slot
            if (equipment && !item)
            {
                //Check it Matches Slot
                if (newItem.slot == slot)
                {
                    // Set Parents ItemSlot to Null
                    parent.item = null;
                    // Set New Items Parent to This
                    _object.transform.SetParent(this.transform);
                    item = newItem;
                    ResetLocals(item);
                }
                else
                {
                    // Do Nothing - Maybe a Text Message
                }
            }
            // Filled Equipment Slot
            else if (equipment && item)
            {
                // Check it Matches Slot
                if (newItem.slot == slot)
                {
                    // Set Current Items Parent as Bag Slot
                    item.gameObject.transform.SetParent(parent.transform);
                    parent.item = item;
                    // Set New Items Parent to This
                    _object.transform.SetParent(this.transform);
                    item = newItem;
                    ResetLocals(item, parent.item);
                }
                else
                {
                    // Do Nothing - Maybe a Text Message
                }
            }
            // Equipment Slot to Bags
            else if (!equipment && parent.equipment)
            {
                // If Bag Slot is Empty
                if (!item)
                {
                    // Set New Items Parent to This
                    _object.transform.SetParent(this.transform);
                    item = newItem;
                    parent.item = null;
                    ResetLocals(item);
                }
                // Swapping w/ Same Type
                else if (item && item.slot == parent.slot)
                {
                    // Set Current Items Parent as Equipment
                    item.transform.SetParent(parent.transform);
                    parent.item = item;
                    // Set New Items Parent to This
                    _object.transform.SetParent(this.transform);
                    item = newItem;
                    ResetLocals(item, parent.item);
                }
                // Not Empty & Not Matching
                else
                {
                    // Do Nothing - Maybe a Text Message
                }
            }
            // Swapping Bag Slots
            else if (!equipment && !parent.equipment)
            {
                // If Bag Slot is Empty
                if (!item)
                {
                    // Set New Items Parent to This
                    _object.transform.SetParent(this.transform);
                    item = newItem;
                    parent.item = null;
                    ResetLocals(item);
                }
                // Else Another Item - Swap
                else if (item)
                {
                    // Set Current Items Parent as Equipment
                    item.transform.SetParent(parent.transform);
                    parent.item = item;
                    // Set New Items Parent to This
                    _object.transform.SetParent(this.transform);
                    item = newItem;
                    ResetLocals(item, parent.item);
                }
                else
                {
                    // Do Nothing - Maybe a Text Message
                    Debug.LogError("Hit ItemSwitching Else");
                }
            }
            else
            {
                // Don't think this should ever fire
                Debug.LogError("Hit ItemSwitching Else");
            }
        }
    }

    public void SetId(int _id)
    {
        id = _id;
    }
}
