using System;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public static CardsManager _Instance { get; private set; }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(this);
        else
            _Instance = this;
    }

    public static Action<Card> cardClick = null;

    public void OnCardClick(Card card)
    {
        if (cardClick == null)
        {
            Debug.LogError("No Subscribers For Event");
            return;
        }
        cardClick.Invoke(card);
    }

     public static Action cardsReady = null;

    public void OnCardsReady()
    {
        if (cardsReady == null)
        {
            Debug.LogError("No Subscribers For Event");
            return;
        }
        cardsReady.Invoke();
    }
}
