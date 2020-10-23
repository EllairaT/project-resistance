using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    public Card card;
    public Text numberSpawned;
    public TextMeshProUGUI cost;

    private void Start()
    {
        numberSpawned.text = card.numberSpawned.ToString();
        cost.SetText("g " + card.Cost.ToString());
    }
}
