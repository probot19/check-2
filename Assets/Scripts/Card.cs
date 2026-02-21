using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private RectTransform _RectTransform;
    public void Init(float width, float height, float posX, float posY)
    {
        _RectTransform.sizeDelta = new Vector2(width, height);
        _RectTransform.anchoredPosition = new Vector2(posX, posY);
    }
}
