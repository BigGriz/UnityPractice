using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ability : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public int id;
    private Cooldown cooldownUI;
    [Header("Setup Fields")]
    public Sprite abilitySprite;
    public GameObject projectilePrefab;
    [Header("Ability Stats")]
    public float cooldown;
    new public string name;

    public Vector3 local;
    public bool dragging;

    #region Setup
    private void Awake()
    {
        cooldownUI = GetComponentInChildren<Cooldown>();
        GetComponent<Image>().sprite = abilitySprite;
        cooldownUI.gameObject.GetComponent<Image>().sprite = abilitySprite;
    }
    private void Start()
    {   
        // Set Event Callback
        GameEvents.instance.useAbility += UseAbility;
        GameEvents.instance.setCooldowns += SetCooldowns;
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.useAbility -= UseAbility;
        GameEvents.instance.setCooldowns -= SetCooldowns;
    }
    #endregion Setup

    // Button Press
    public void ButtonUse()
    {
        if (!dragging)
        {
            UseAbility(this.id, Player.instance.targeting.target);
        }
    }

    // Use Ability
    public void UseAbility(int _id, GameObject _target)
    {
        if (_id == this.id)
        {
            // Check if Target & Off Cooldown
            if (cooldownUI.Ready() && _target)
            {
                // Check for Line of Sight
                if (CheckLOS(_target))
                {
                    GameEvents.instance.SetCooldowns(this.name);
                    Projectile temp = Instantiate(projectilePrefab, Player.instance.transform.position, Player.instance.transform.rotation).GetComponent<Projectile>();
                    temp.Seek(_target);
                }
            }
            // Still on CD
            else if (!cooldownUI.Ready())
            {
                Debug.Log("On Cooldown");
            }
            // No Target
            else
            {
                Debug.Log("No Target");
            }
        }
    }

    // Set Cooldowns
    public void SetCooldowns(string _name)
    {
        if (this.name == _name)
        {
            cooldownUI.Begin(cooldown);
        }
    }

    // Ensure LOS to Target
    bool CheckLOS(GameObject _target)
    {
        // Grab all Objects between Target & Self
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Player.instance.transform.position, _target.transform.position - Player.instance.transform.position, Mathf.Infinity);
        // Check if any are Environmental/Blocking
        for (int i = 0; i < hits.Length; i++)
        {
            // If so Return False
            if (hits[i].collider.gameObject.tag == "Environment")
            {
                return (false);
            }
        }
        // Else have LOS
        return (true);
    }

    // Dragging
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
        // Check for Ability Slots
        GameEvents.instance.SetAbility(this.gameObject);
        // If Shifting from Ability Bar
        if (!transform.parent.GetComponent<AbilitySlot>().spellBook)
        {
            GameEvents.instance.ClearAbility(id);
        }
    }
}
