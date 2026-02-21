using TMPro;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Score;
    [SerializeField] private TextMeshProUGUI _Turns;

    private int mScore;
    private int mTurn;
    private int mComboStreak;
    private int priviousScore;

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
    mComboStreak++;

    int comboBonus = Mathf.Max(0, mComboStreak - 1);

    mScore += 1 + comboBonus;

    _Score.text = mScore.ToString();

}


    private void OnTurn()
{
    mTurn++;
    _Turns.text = mTurn.ToString();

    if (priviousScore == mScore)
    {
        mComboStreak = 0;
    }

    priviousScore = mScore;
}

}
