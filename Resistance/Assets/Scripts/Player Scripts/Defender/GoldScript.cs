using UnityEngine;
using TMPro;

public class GoldScript : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    //Set the gold for the player
    public void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }
}
