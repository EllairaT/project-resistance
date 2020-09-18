using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loadout : MonoBehaviour
{
    protected PlayerScript Owner { get; set; }
    protected AddOns[] loadOut;

    void Start()
    {
        if (Owner.role == "Card Master")
        {
            
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

}
