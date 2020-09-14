using UnityEngine;
using TMPro;

public class GoldScript : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    public void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }
}
