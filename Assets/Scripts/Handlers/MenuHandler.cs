using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    [SerializeField] private Button _StartButton;

    private int mRowSize;
    private int mColumnSize;

    void Start()
    {
        _StartButton.interactable = false;
    }

    public void OnInputRow(string input)
    {
        if (input.NullIfEmpty() == null)
        {
            _StartButton.interactable = false;
            return;
        }
        mRowSize = int.Parse(input);
        CheckGridSize();
    }

    public void OnInputColumn(string input)
    {
        if (input.NullIfEmpty() == null)
        {
            _StartButton.interactable = false;
            return;
        }
        mColumnSize = int.Parse(input);
        CheckGridSize();
    }

    public void OnPlayButtonClick()
    {
        CardsManager._Instance.OnStartGame(mRowSize, mColumnSize);
        gameObject.SetActive(false);
    }


    private void CheckGridSize()
    {
        if (mRowSize > 0 && mColumnSize > 0 && mRowSize * mColumnSize % 2 == 0)
            _StartButton.interactable = true;
    }
}
