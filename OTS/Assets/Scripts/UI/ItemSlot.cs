using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public int id;
    public bool spellBook;

    private void Start()
    {
        if (GetComponentInChildren<Ability>())
        {
            item = GetComponentInChildren<Item>();
        }
        // Set Event Callback
       /* GameEvents.instance.setAbility += SetAbility;
        GameEvents.instance.clearAbility += ClearAbility;*/
    }
    private void OnDestroy()
    {
        // Cleanup Callbacks
       /* GameEvents.instance.setAbility -= SetAbility;
        GameEvents.instance.clearAbility -= ClearAbility;*/
    }


    public void SetId(int _id)
    {
        id = _id;
        if (item)
        {
            item.id = id;
        }
    }

    public void SetAbility(int _id, GameObject _object)
    {
        if (id == _id)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }

            Item temp = Instantiate(_object, this.transform).GetComponent<Item>();
            if (temp)
            {
                item = temp;
                item.id = id;
            }
        }
    }

    public void ClearAbility(int _id)
    {
        if (id == _id)
        {
            Destroy(item.gameObject);
            item = null;
        }
    }
}
