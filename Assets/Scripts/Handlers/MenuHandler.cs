using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    [SerializeField] private Button _StartButton;
    [SerializeField] private AudioSource _PlayAudio;

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
        _PlayAudio.Play();
        CardsManager._Instance.OnStartGame(mRowSize, mColumnSize);
        StartCoroutine(DisableAfterSound());
    }

    IEnumerator DisableAfterSound()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }



    private void CheckGridSize()
    {
        if (mRowSize > 0 && mColumnSize > 0 && mRowSize * mColumnSize % 2 == 0 && mRowSize * mColumnSize < 88)  // This project has 44 images for we can build only 44 x 2 cards
            _StartButton.interactable = true;
    }
}
