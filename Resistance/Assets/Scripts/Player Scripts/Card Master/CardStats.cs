using TMPro;
using UnityEngine;

public class CardStats : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI numberOfMonstersAvailable;
    public TextMeshProUGUI cost;

    private int remainingAvailable;

    private void Start()
    {
        numberOfMonstersAvailable.text = card.maxNumber.ToString();
        cost.SetText("g " + card.Cost.ToString());
        remainingAvailable = card.maxNumber;
    }

    public void UpdateNumberOfAvailable(int i)
    {
        remainingAvailable += i;
        Debug.Log(remainingAvailable);
        numberOfMonstersAvailable.SetText(remainingAvailable.ToString());
    }
}
