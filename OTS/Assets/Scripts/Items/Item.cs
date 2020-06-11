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
    public Show properties;

    //[Header("Setup Fields")]
    [DrawIf("properties", Show.Setup)]
    public Sprite itemSprite;
    [DrawIf("properties", Show.Setup)]
    public bool equipped;
    [Header("Item Stats")]
    public ItemStats itemStats;
    new public string name;
    public int tier;
    public ItemType type;
    public EquipSlot slot;

    // Drag n Drop
    private Vector3 local;
    private bool dragging;

    private void Start()
    {
        itemStats = ScriptableObject.CreateInstance("ItemStats") as ItemStats;
        itemStats.Setup(slot, tier);
    }

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
