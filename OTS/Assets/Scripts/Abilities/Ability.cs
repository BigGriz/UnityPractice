using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ability : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public int id;
    [Header("Setup Fields")]
    public Sprite abilitySprite;
    public GameObject projectilePrefab;
    public Image imageCooldown;
    // SO for Cooldowns
    public AbilityStats stats;
    [Header("Ability Stats")]
    public float cooldown;
    public float manaCost;
    new public string name;
    // Drag n Drop
    private Vector3 local;
    private bool dragging;

    public List<AbilityMods> mods;

    #region Setup
    private void Awake()
    {
        GetComponent<Image>().sprite = abilitySprite;
        imageCooldown.sprite = abilitySprite;
    }
    private void Start()
    {   
        // Set Event Callback
        GameEvents.instance.useAbility += UseAbility;
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.useAbility -= UseAbility;
    }
    #endregion Setup

    private void Update()
    {
        imageCooldown.fillAmount = stats.cooldown / cooldown;
    }

    public bool Ready()
    {
        return (stats.cooldown <= 0);
    }

    // Button Press
    public void ButtonUse()
    {
        if (!dragging)
        {
            UseAbility(this.id, Player.instance.targeting.target);
        }
    }

    public void GetMod(ModSlot _slot)
    {
        if (_slot.mod)
        {
            mods.Add(_slot.mod);
        }
    }

    // Use Ability
    public void UseAbility(int _id, GameObject _target)
    {
        if (_id == this.id)
        {
            // Check if Target & Off Cooldown
            if (Ready() && _target)
            {
                // Check for Line of Sight
                if (CheckLOS(_target))
                {
                    GameEvents.instance.SetCooldowns(this.name, this.cooldown);
                    Player.instance.SpendMana(manaCost);
                    Projectile temp = Instantiate(projectilePrefab, Player.instance.transform.position, Player.instance.transform.rotation).GetComponent<Projectile>();
                    temp.Setup(mods);
                    temp.Seek(_target);
                }
            }
            // Still on CD
            else if (!Ready())
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
