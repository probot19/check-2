using UnityEngine;

public class CardsHandler : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private RectTransform _Container;
    [SerializeField] private GameObject _CardPrefab;
    [SerializeField] private int _Columns;
    [SerializeField] private int _Rows;
    [SerializeField] private float _Spacing;

    void Start()
    {
        SpawnCards();
    }

    private void SpawnCards()
    {
        float containerWidth = _Container.rect.width;
        float containerHeight = _Container.rect.height;

        float cardWidth = (containerWidth - (_Spacing * (_Columns - 1))) / _Columns;
        float cardHeight = (containerHeight - (_Spacing * (_Rows - 1))) / _Rows;

        float gridWidth = _Columns * cardWidth + (_Columns - 1) * _Spacing;
        float gridHeight = _Rows * cardHeight + (_Rows - 1) * _Spacing;

        float startX = -gridWidth / 2 + cardWidth / 2;
        float startY = gridHeight / 2 - cardHeight / 2;

        int cardIndex = 0;

        for (int row = 0; row < _Rows; row++)
        {
            for (int column = 0; column < _Columns; column++)
            {
                if (cardIndex >= this._Columns * this._Rows)
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
