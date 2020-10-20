using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentCard : MonoBehaviour
{
    public TextMeshProUGUI cost;
    public TextMeshProUGUI count;

    public void UpdateCurrentCardDetails(Card c)
    {
        cost.SetText("g " + c.Cost);
        count.SetText(c.minNumber.ToString());
    }
}
