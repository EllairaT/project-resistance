using TMPro;
using UnityEngine;

public class CardStats : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI numberOfMonstersAvailable;
    public TextMeshProUGUI cost;

    public static GameObject itemBeingUpdated;
    private void Awake()
    {
      // CurrentCard.changeEvent += UpdateNumberOfAvailable;
    }
    private void Start()
    {
        numberOfMonstersAvailable.text = card.maxNumber.ToString();
        cost.SetText("g " + card.Cost.ToString());
    }

    public void UpdateNumberOfAvailable(int i)
    {
        Debug.Log("did i even make it here?");
        numberOfMonstersAvailable.SetText(i.ToString());
    }

}
