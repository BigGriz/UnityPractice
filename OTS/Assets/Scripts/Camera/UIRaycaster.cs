using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRaycaster : MonoBehaviour
{
    [Header("UI Raycasting")]
    //[HideInInspector]
    public GraphicRaycaster m_Raycaster;
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

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            //Debug.Log(result.gameObject.name);

            // Check it's an item hovered over
            AbilitySlot temp = result.gameObject.GetComponent<AbilitySlot>();
            if (temp)
            {
                GameEvents.instance.mouseOver = temp;
                return;
            }
        }
        GameEvents.instance.mouseOver = null;
    }
}
