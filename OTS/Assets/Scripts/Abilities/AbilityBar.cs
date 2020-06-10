using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBar : MonoBehaviour
{
    public List<GameObject> actionBar;


    // Start is called before the first frame update
    void Start()
    {
        int id = 1;
        foreach (Transform child in transform)
        {
            child.GetComponent<Ability>().id = id;
            actionBar.Add(child.gameObject);
            id++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
