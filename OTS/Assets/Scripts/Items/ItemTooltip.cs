using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Item Referred To
    private Item item;
    private ItemStats itemStats;
    // Textbox Elements
    private GameObject textBox;
    private TMPro.TextMeshProUGUI nameText;
    private TMPro.TextMeshProUGUI prefixText;
    private TMPro.TextMeshProUGUI suffixText;

    //public List<object> list;

    private void Start()
    {
        item = GetComponent<Item>();
        itemStats = item.itemStats;

        // Probably needs rework - Saves time on setting up in editor
        textBox = transform.GetChild(0).gameObject;
        List<TMPro.TextMeshProUGUI> tempList = new List<TMPro.TextMeshProUGUI>();
        foreach (Transform n in textBox.transform)
        {
            tempList.Add(n.gameObject.GetComponent<TMPro.TextMeshProUGUI>());
        }
        nameText = tempList[0];
        prefixText = tempList[1];
        suffixText = tempList[2];
        tempList.Clear();


        textBox.SetActive(false);
        SetText();
    }

    // Adds Affix Names to Text - Change this later to reflect stats
    public void SetText()
    {
        List<object> list = itemStats.GetStats();

        nameText.SetText(item.name);
        string preText = "";
        string sufText = "";

        var fields = typeof(Affix).GetFields();
        // Skipping Name & Type
        for (int i = 2; i < list.Count - 1; i++)
        {
            if ((AffixType)list[0] == AffixType.Prefix)
            {
                if (CheckType(list[i]))
                {
                    preText += " " + fields[i].Name;
                    preText += ": " + list[i];
                }
            }
            else
            {
                if (CheckType(list[i]))
                {
                    sufText += " " + fields[i].Name;
                    sufText += ": " + list[i];
                }
            }
        }

        prefixText.SetText(preText);
        suffixText.SetText(sufText);
    }

    bool CheckType(object _object)
    {
        if (_object.GetType() == typeof(float) && (float)_object != 0.0f)
        {
            return true;
        }
        if (_object.GetType() == typeof(int) && (int)_object != 0)
        {
            return true;
        }
        if (_object.GetType() == typeof(bool) && (bool)_object == true)
        {
            return true;
        }

        return false;
    }

    public void ShowTooltip()
    {
        textBox.SetActive(true);
    }

    public void HideTooltip()
    {
        textBox.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }
}
