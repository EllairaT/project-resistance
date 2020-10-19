using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNumber : MonoBehaviour
{
    public Card card;
    public Text numberSpawned;

    private void Start()
    {
        card = transform.parent.GetComponent<Draggable>().card;
        numberSpawned.text = card.numberSpawned.ToString();
    }
}
