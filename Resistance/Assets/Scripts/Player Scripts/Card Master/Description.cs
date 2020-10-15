using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Description : BaseMonobehaviour
{
    public Card card;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI desc; 

    public void ShowDescription()
    {
        Name.SetText(card.Name);
        cost.SetText(" g" + card.Cost);
        desc.SetText(card.Description);
        type.SetText(card.type.ToString());
    }
}
