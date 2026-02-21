using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsHandler : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private GameObject _CardPrefab;
    [SerializeField] private GameObject _ClickBlocker;
    [SerializeField] private RectTransform _Container;
    [SerializeField] private SpriteConfig _SpriteConfig;
    [SerializeField] private int _Columns;
    [SerializeField] private int _Rows;

    private List<Card> mCards = new List<Card>();
    private List<Card> mSelectedCards = new List<Card>();
    private const int CardSelectionCount = 2;
    private const float _Spacing = 10;

    void Awake()
    {
        CardsManager.cardClick += OnCardClick;
        CardsManager.cardsReady += OnCardsReady;
    }

    void OnDestroy()
    {
        CardsManager.cardClick -= OnCardClick;
        CardsManager.cardsReady -= OnCardsReady;
    }

    private void OnCardClick(Card card)
    {
        if (mSelectedCards.Count >= CardSelectionCount)
        {
            Debug.LogError("OnCardClick : Somthing went worng");
            return;
        }

        mSelectedCards.Add(card);

        if (mSelectedCards.Count == CardSelectionCount)
        {
            StartCoroutine(ResolveSelection());
        }
    }

    private void OnCardsReady()
    {
        _ClickBlocker.SetActive(false);
    }

    IEnumerator ResolveSelection()
    {
        _ClickBlocker.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        if (mSelectedCards[0].GetIndex() == mSelectedCards[1].GetIndex())
        {
            mSelectedCards[0].Dumped();
            mSelectedCards[1].Dumped();

            HUDManager._Instance.OnCardsMatched();
        }
        else
        {
            mSelectedCards[0].Reset();
            mSelectedCards[1].Reset();
        }

        mSelectedCards.Clear();

        HUDManager._Instance.OnTurn();
        _ClickBlocker.SetActive(false);
    }


    void Start()
    {
        SpawnCards();
        _ClickBlocker.SetActive(true);
    }

    private void SpawnCards()
    {

        if (_Columns * _Rows % 2 != 0)
        {
            Debug.LogError("Needs even number of cards");
            return;
        }

        float containerWidth = _Container.rect.width;
        float containerHeight = _Container.rect.height;

        float cardWidth = (containerWidth - (_Spacing * (_Columns - 1))) / _Columns;
        float cardHeight = (containerHeight - (_Spacing * (_Rows - 1))) / _Rows;

        float gridWidth = _Columns * cardWidth + (_Columns - 1) * _Spacing;
        float gridHeight = _Rows * cardHeight + (_Rows - 1) * _Spacing;

        float startX = -gridWidth / 2 + cardWidth / 2;
        float startY = gridHeight / 2 - cardHeight / 2;

        InstantiateCards(startX, startY, cardWidth, cardHeight);
    }

    private void InstantiateCards(float startX, float startY, float cardWidth, float cardHeight)
    {
        int cardIndex = 0;
        List<int> pairIds = GeneratePairIDs(_Columns * _Rows);

        for (int row = 0; row < _Rows; row++)
        {
            for (int column = 0; column < _Columns; column++)
            {
                if (cardIndex >= _Columns * _Rows)
                    return;

                GameObject card = Instantiate(_CardPrefab, _Container);
                Card newCard = card.GetComponent<Card>();
                mCards.Add(newCard);

                float posX = startX + column * (cardWidth + _Spacing);
                float posY = startY - row * (cardHeight + _Spacing);
                Sprite sprite = _SpriteConfig.sprites[pairIds[cardIndex]];
                newCard.Init(pairIds[cardIndex], cardWidth, cardHeight, posX, posY, sprite);

                cardIndex++;
            }
        }
    }

    private List<int> GeneratePairIDs(int totalCards)
    {
        List<int> ids = new List<int>();

        int pairCount = totalCards / 2;

        for (int i = 0; i < pairCount; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        for (int i = ids.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            int temp = ids[i];
            ids[i] = ids[rand];
            ids[rand] = temp;
        }

        return ids;
    }

}
