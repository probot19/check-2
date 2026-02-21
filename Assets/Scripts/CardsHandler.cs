using UnityEngine;

public class CardsHandler : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private RectTransform _Container;
    [SerializeField] private GameObject _CardPrefab;
    [SerializeField] private int _TotalCards;
    [SerializeField] private float _Spacing;

    void Start()
    {
        SpawnCards();
    }

    private void SpawnCards()
    {
        float containerWidth = _Container.rect.width;
        float containerHeight = _Container.rect.height;

        int columns = Mathf.CeilToInt(Mathf.Sqrt(_TotalCards));
        int rows = Mathf.CeilToInt((float)_TotalCards / columns);

        float cardWidth = (containerWidth - (_Spacing * (columns - 1))) / columns;
        float cardHeight = (containerHeight - (_Spacing * (rows - 1))) / rows;

        float gridWidth = columns * cardWidth + (columns - 1) * _Spacing;
        float gridHeight = rows * cardHeight + (rows - 1) * _Spacing;

        float startX = -gridWidth / 2 + cardWidth / 2;
        float startY = gridHeight / 2 - cardHeight / 2;

        int cardIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (cardIndex >= _TotalCards)
                    return;

                GameObject card = Instantiate(_CardPrefab, _Container);
                Card newCard = card.GetComponent<Card>();

                float posX = startX + column * (cardWidth + _Spacing);
                float posY = startY - row * (cardHeight + _Spacing);

                newCard.Init(cardWidth, cardHeight, posX, posY);

                cardIndex++;
            }
        }
    }
}
