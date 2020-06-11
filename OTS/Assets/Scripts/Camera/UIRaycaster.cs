using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRaycaster : MonoBehaviour
{
    [Header("UI Raycasting")]
    public GraphicRaycaster abilityRaycaster;
    public GraphicRaycaster itemRaycaster;
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

        CheckItems();
        CheckAbilities();   
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

    void CheckItems()
    {
        //Create a list of Raycast Results
        List<RaycastResult> itemResults = new List<RaycastResult>();
        itemRaycaster.Raycast(m_PointerEventData, itemResults);
        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult n in itemResults)
        {
            //Debug.Log(result.gameObject.name);

            // Check it's an item hovered over
            ItemSlot temp = n.gameObject.GetComponent<ItemSlot>();
            if (temp)
            {
                GameEvents.instance.mouseOverItem = temp;
                return;
            }
        }
        GameEvents.instance.mouseOverItem = null;
    }

}
