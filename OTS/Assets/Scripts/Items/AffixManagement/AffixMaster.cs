using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffixMaster : MonoBehaviour
{
    public static AffixMaster instance;
    // Setup All Affixes - Eventually Swap to Using Multiple Tables
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AffixMaster exists!");
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        LoadTable(tablename);
        MakeAffixes();
        SortAffixes();
    }

    [Header("Table to Use")]
    public string tablename;
    [HideInInspector] public ValueTable[] values;
    [HideInInspector] public List<Affix> affixes;
    [Header("Affix Lists")]
    public List<Affix> prefixes;
    public List<Affix> suffixes;

    public void SortAffixes()
    {
        foreach (Affix n in affixes)
        {
            if (n.type == AffixType.Prefix)
            {
                prefixes.Add(n);
            }
            else if (n.type == AffixType.Suffix)
            {
                suffixes.Add(n);
            }
        }

        affixes.Clear();
    }

    public void LoadTable(string table)
    {
        string temp = "Assets/Resources/" + table + ".csv";
        fgCSVReader.LoadFromFile(temp, new fgCSVReader.ReadLineDelegate(FillTable));
    }

    public void FillTable(int line_index, List<string> line)
    {
        if (line_index == 0)
        {
            if (values.Length < line.Count)
            {
                values = new ValueTable[line.Count];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = new ValueTable();
                }
            }
        }

        if (line_index == 0)
        {
            for (int i = 0; i < line.Count; i++)
            {
                values[i].name = line[i];
            }
        }
        else
        {
            for (int i = 0; i < line.Count; i++)
            {
                if (values[i].values == null)
                {
                    values[i].values = new List<string>();
                    values[i].values.Add(line[i]);
                }
                else
                {
                    values[i].values.Add(line[i]);
                }
            }
        }

    }

    public void MakeAffixes()
    {
        // Check # of named items
        for (int i = 0; i < values[0].values.Count; i++)
        {
            ConvertToAffix(i);
        }
    }

    public void ConvertToAffix(int _row)
    {
        Affix temp = ScriptableObject.CreateInstance("Affix") as Affix;
        // Get Values
        for (int i = 0; i < values.Length; i++)
        {
            switch (values[i].name)
            {
                // Affix Type
                case "Prefix":
                    {
                        bool tempBool = ConvertToBool(values[i].values[_row]);
                        if (tempBool)
                        {
                            temp.type = AffixType.Prefix;
                        }
                        else
                        {
                            temp.type = AffixType.Suffix;
                        }
                        break;
                    }
                case "Name":
                    {
                        temp.name = values[i].values[_row];
                        break;
                    }
                // Damage Stats
                case "BaseDamage":
                    {
                        temp.baseDamage = ConvertToInt(values[i].values[_row]);
                        break;
                    }
                case "PercentDamage":
                    {
                        temp.percentDamage = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                case "AttackSpeed":
                    {
                        temp.atkSpeed = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                case "BaseCrit":
                    {
                        temp.baseCrit = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                case "PercentCrit":
                    {
                        temp.percentCrit = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                // Resource Stats
                case "EnergyPerSec":
                    {
                        temp.energyPerSec = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                case "EnergyOnKill":
                    {
                        temp.energyOnKill = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                case "BonusReward":
                    {
                        temp.bonusReward = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                case "BonusXP":
                    {
                        temp.bonusXP = ConvertToFloat(values[i].values[_row]);
                        break;
                    }
                // Abilities
                case "AutoUpgrade":
                    {
                        temp.autoUpgrade = ConvertToBool(values[i].values[_row]);
                        break;
                    }
            }
        }
        affixes.Add(temp);
    }

    public bool ConvertToBool(string _string)
    {
        bool temp;
        bool test = bool.TryParse(_string, out temp);
        if (test)
        {
            return temp;
        }
        else
        {
            Debug.LogError("BadString Conversion to Bool");
            return false;
        }
    }
    public int ConvertToInt(string _string)
    {
        int temp;
        bool test = int.TryParse(_string, out temp);
        if (test)
        {
            return temp;
        }
        else
        {
            Debug.LogError("BadString Conversion to Int");
            return 9999;
        }
    }
    public float ConvertToFloat(string _string)
    {
        float temp;
        bool test = float.TryParse(_string, out temp);
        if (test)
        {
            return temp;
        }
        else
        {
            Debug.LogError("BadString Conversion to Int");
            return 9999;
        }
    }
}
