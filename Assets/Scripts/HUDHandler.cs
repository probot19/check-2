using TMPro;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Score;
    [SerializeField] private TextMeshProUGUI _Turns;

    private int mScore = 0;
    private int mTurn = 0;

    void Awake()
    {
        HUDManager.cardsMatched += OnCardsMatch;
        HUDManager.turn += OnTurn;
    }

    void OnDestroy()
    {
        HUDManager.cardsMatched -= OnCardsMatch;
        HUDManager.turn -= OnTurn;
    }

    void Start()
    {
        _Score.text = mScore.ToString();
        _Turns.text = mTurn.ToString();
    }

    private void OnCardsMatch()
    {
        mScore++;
        _Score.text = mScore.ToString();
    }

    private void OnTurn()
    {
        mTurn++;
        _Turns.text = mTurn.ToString();
    }
}
