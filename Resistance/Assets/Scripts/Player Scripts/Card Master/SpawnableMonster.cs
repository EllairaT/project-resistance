using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableMonster : MonoBehaviour
{

    public CardSystem cardSystem;
    public bool isPlaced;

    public void Place()
    {
        Instantiate(gameObject, transform.position, transform.rotation);     
    }
}

