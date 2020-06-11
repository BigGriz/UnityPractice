using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ItemType
{
    None,
    Equipment,
    Consumable,
    Trash,
    Quest
}

public enum EquipSlot
{
    None,
    Head,
    Neck,
    Shoulder,
    Body,
    Cape,
    Shirt,
    Tabard,
    Bracers
}


public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Setup Fields")]
    public Sprite itemSprite;
    public bool equipped;
    //public ItemStats itemStats;
    [Header("Item Stats")]
    new public string name;
    public ItemType type;
    public EquipSlot slot;
    // Drag n Drop
    private Vector3 local;
    private bool dragging;



    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        local = GetComponent<RectTransform>().anchoredPosition;
        GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            transform.position = Input.mousePosition;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        GetComponent<RectTransform>().anchoredPosition = local;
        dragging = false;
        // Check if Equipment
        if (type == ItemType.Equipment)
        {
            GameEvents.instance.SetItem(this.gameObject);
        }
    }
}
