using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRaycaster : MonoBehaviour
{
    [Header("UI Raycasting")]
    public GraphicRaycaster abilityRaycaster;
    public GraphicRaycaster equipmentRaycaster;
    public GraphicRaycaster backpackRaycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private void Start()
    {
        // Setup new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
    }

    // Update is called once per frame
    void Update()
    {
        //Set Pointer Event Position to mouse position
        m_PointerEventData.position = Input.mousePosition;

        CheckEquipment();
        CheckAbilities();
        CheckBackPack();
    }

    void CheckAbilities()
    {
        //Create a list of Raycast Results
        List<RaycastResult> abilityResults = new List<RaycastResult>();
        abilityRaycaster.Raycast(m_PointerEventData, abilityResults);
        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult n in abilityResults)
        {
            //Debug.Log(result.gameObject.name);

            // Check it's an item hovered over
            AbilitySlot temp = n.gameObject.GetComponent<AbilitySlot>();
            if (temp)
            {
                GameEvents.instance.mouseOverAbility = temp;
                return;
            }
        }
        GameEvents.instance.mouseOverAbility = null;
    }

    void CheckEquipment()
    {
        //Create a list of Raycast Results
        List<RaycastResult> equipmentResults = new List<RaycastResult>();
        equipmentRaycaster.Raycast(m_PointerEventData, equipmentResults);
        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult n in equipmentResults)
        {
            //Debug.Log(result.gameObject.name);

            // Check it's an item hovered over
            ItemSlot temp = n.gameObject.GetComponent<ItemSlot>();
            if (temp)
            {
                GameEvents.instance.mouseOverEquipment = temp;
                return;
            }
        }
        GameEvents.instance.mouseOverEquipment = null;
    }

    void CheckBackPack()
    {
        //Create a list of Raycast Results
        List<RaycastResult> backpackResults = new List<RaycastResult>();
        backpackRaycaster.Raycast(m_PointerEventData, backpackResults);
        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult n in backpackResults)
        {
            //Debug.Log(result.gameObject.name);

            // Check it's an item hovered over
            ItemSlot temp = n.gameObject.GetComponent<ItemSlot>();
            if (temp)
            {
                GameEvents.instance.mouseOverBackpack = temp;
                return;
            }
        }
        GameEvents.instance.mouseOverBackpack = null;
    }

}
