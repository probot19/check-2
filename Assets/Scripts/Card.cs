using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private RectTransform _RectTransform;
    // [SerializeField] private TextMeshProUGUI _id;
    [SerializeField] private Image _ItemImage;
    private int mId;
    private CardState mState;

    public void Init(int id,float width, float height, float posX, float posY, Sprite sprite)
    {
        mId = id;
        // _id.SetText(id.ToString());
        _ItemImage.sprite = sprite;
        _RectTransform.sizeDelta = new Vector2(width, height);
        _RectTransform.anchoredPosition = new Vector2(posX, posY);
        mState = CardState.IDLE;
    }

    public int GetIndex()
    {
        return mId;
    }

    public void Reset()
    {
        mState = CardState.IDLE;
    }

    public CardState GetState()
    {
        return mState;
    }

    public void Dumped()
    {
        mState = CardState.DUMPED;
        _RectTransform.localScale = Vector2.zero;
    }

    #region UI Callback
    public void OnClick()
    {
        if(mState == CardState.SELECTED)
        {
            return;
        }
        mState = CardState.SELECTED;
        CardsManager._Instance.OnCardClick(this);
    }
    #endregion
}


public enum CardState
{
    NONE,
    IDLE,
    SELECTED,
    DUMPED
}