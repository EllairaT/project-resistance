using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStructure : MonoBehaviour
{
    public Structure[] structureArray;
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowList()
    {
        foreach (Structure s in structureArray)
        {
            //Debug.Log(s.structurePrefab.name);
        }
    }
}
