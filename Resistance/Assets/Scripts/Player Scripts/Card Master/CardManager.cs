using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager: MonoBehaviour
{
    public CardSystem cs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonsters();
        }        
    }

    public void SpawnMonsters()
    {
        cs.SpawnMonsters();
    }
}
