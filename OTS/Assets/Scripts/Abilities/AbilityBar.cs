using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBar : MonoBehaviour
{
    public List<GameObject> actionBar;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            actionBar.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
