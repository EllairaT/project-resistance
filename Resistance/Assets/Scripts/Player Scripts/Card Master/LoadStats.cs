using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStats : MonoBehaviour
{
    [SerializeField] private Card script;

   // [SerializeField] private Attackable attackable; //reference for the attackable script that all monsters need

    void Start()
    {
       // attackable = GetComponent<Attackable>();
       // attackable.health = script.Health; //set health for monster
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float TakeDamage(float dmg)
    {
        dmg = script.CalculateDamageTaken(dmg);
        return dmg;
    }
}
