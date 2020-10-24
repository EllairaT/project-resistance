using Boo.Lang;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CardSystem cardSystem;

    [Header("Text")]
    public TextMeshProUGUI cost;
    public TextMeshProUGUI count;
    public TextMeshProUGUI type;
    public TextMeshProUGUI description;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI baseCost;

    [Header("UI Elements")]
    public GameObject infoPanel;
    public Button[] functionPanelButtons;
    public CardSlot slot;
    private Card card;
    private CardStats stats;

    private void Awake()
    {
        infoPanel.SetActive(false);
        CardSlot.changeEvent += SlotChanged;
        cardSystem = transform.root.GetComponent<CardSystem>();
    }

    private void SlotChanged(bool b, Card c, CardStats cs)
    {
        UpdateCurrentCardDetails(c, cs);
    }

    public bool IsCurrentCardSlotEmpty()
    {
        if (slot.transform.childCount > 0)
        {
            return false;
        }
        return true;
    }

    public void UpdateCurrentCardDetails(Card c, CardStats cs)
    {
        card = c;
        stats = cs;

        if (card != null)
        {

            Name.SetText(c.Name);
            type.SetText(c.type.ToString());
            description.SetText(c.Description);
            baseCost.SetText(c.Cost.ToString());

            UpdateCardCost(c.minNumber);
            count.SetText(c.minNumber.ToString());

            cardSystem.numberToSpawn = 1;
            cardSystem.Spawnable = card.prefab;
        }
        if (IsCurrentCardSlotEmpty())
        {
            ResetCardStats();
        }
        ToggleButtons();
    }

    private void ToggleButtons()
    {
        if (IsCurrentCardSlotEmpty())
        {
            foreach (Button btn in functionPanelButtons)
            {
                btn.interactable = false;
            }
        }
        else
        {
            foreach (Button btn in functionPanelButtons)
            {
                btn.interactable = true;
            }
        }
    }

    public void ResetCardStats()
    {
        cost.SetText("0");
        baseCost.SetText("");
        count.SetText("0");
        type.SetText("");
        description.SetText("");
        Name.SetText("");
    }

    public void UpdateCardCount(Button b)
    {
        if (card != null)
        {
            int i = int.Parse(count.GetParsedText());
            if (b.name == "up")
            {
                if (i < card.maxNumber)
                {
                    i++;
                    UpdateAvailableCards(-1);
                }
            }
            else
            {
                if (i > card.minNumber)
                {
                    i--;
                    UpdateAvailableCards(1);
                }
            }

            UpdateCardCost(i);

            count.SetText(i.ToString());
            cardSystem.numberToSpawn = i;
        }
    }

    public void UpdateAvailableCards(int n)
    {
        if (stats != null)
        {
            stats.UpdateNumberOfAvailable(n);
        }
    }

    public void UpdateCardCost(int cardCount)
    {
        int c = card.Cost * cardCount;
        cost.SetText(c.ToString());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!slot.isSlotEmpty)
        {
            infoPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!slot.isSlotEmpty)
        {
            infoPanel.SetActive(false);
        }
    }
}
