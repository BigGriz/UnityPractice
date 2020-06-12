using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour
{
    public Ability ability;
    public int id;
    public bool spellBook;

    private void Start()
    {
        if (GetComponentInChildren<Ability>())
        {
            ability = GetComponentInChildren<Ability>();
        }
        // Set Event Callback
        GameEvents.instance.setAbility += SetAbility;
        GameEvents.instance.clearAbility += ClearAbility;
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
        GameEvents.instance.setAbility -= SetAbility;
        GameEvents.instance.clearAbility -= ClearAbility;
    }


    public void SetId(int _id)
    {
        id = _id;
        if (ability)
        {
            ability.id = id;
        }
    }

    public void SetAbility(int _id, GameObject _object)
    {
        if (id == _id)
        {
            if (ability != null)
            {
                Destroy(ability.gameObject);
            }

            Ability temp = Instantiate(_object, this.transform).GetComponent<Ability>();
            if (temp)
            {
                ability = temp;
                ability.id = id;
                ability.spellBook = false;
            }
        }
    }

    public void ClearAbility(int _id)
    {
        if (id == _id)
        {
            Destroy(ability.gameObject);
            ability = null;            
        }
    }
}
