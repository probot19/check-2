using System;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager _Instance { get; private set; }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(this);
        else
            _Instance = this;
    }

    public static Action cardsMatched = null;

    public void OnCardsMatched()
    {
        if (cardsMatched == null)
        {
            Debug.LogError("No Subscribers For Event");
            return;
        }
        cardsMatched.Invoke();
    }

    public static Action turn = null;

    public void OnTurn()
    {
        if (turn == null)
        {
            Debug.LogError("No Subscribers For Event");
            return;
        }
        turn.Invoke();
    }
}
