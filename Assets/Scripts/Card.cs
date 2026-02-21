using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private RectTransform _RectTransform;
    [SerializeField] private Image _FrontImage;
    [SerializeField] private GameObject _Front;
    [SerializeField] private GameObject _Back;
    private int mId;
    private CardState mState;
    private Coroutine mFlipRoutine = null;

    public void Init(int id, float width, float height, float posX, float posY, Sprite sprite)
    {
        mId = id;
        _FrontImage.sprite = sprite;
        _RectTransform.sizeDelta = new Vector2(width, height);
        _RectTransform.anchoredPosition = new Vector2(posX, posY);
        mState = CardState.IDLE;
        StartCoroutine(WaitForSec(3));
    }

    public int GetIndex()
    {
        return mId;
    }

    public void Reset()
    {
        mState = CardState.IDLE;
        StartCoroutine(Flip(false));
    }

    public void Dumped()
    {
        mState = CardState.DUMPED;

        if (mFlipRoutine != null)
            StopCoroutine(mFlipRoutine);

        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
    }

    private void Show()
    {
        mFlipRoutine = StartCoroutine(Flip(true));
    }

    IEnumerator WaitForSec(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(Flip(false));
        CardsManager._Instance.OnCardsReady();
    }

    IEnumerator Flip(bool showFront)
    {
        float duration = 0.15f;

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            float x = Mathf.Lerp(1, 0, t);
            transform.localScale = new Vector3(x, 1, 1);
            yield return null;
        }

        transform.localScale = new Vector3(0, 1, 1);

        _Front.SetActive(showFront);
        _Back.SetActive(!showFront);

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            float x = Mathf.Lerp(0, 1, t);
            transform.localScale = new Vector3(x, 1, 1);
            yield return null;
        }
    }



    #region UI Callback
    public void OnClick()
    {
        if (mState == CardState.SELECTED)
            return;
        mState = CardState.SELECTED;
        Show();
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