using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    [SerializeField] private Button _StartButton;
    [SerializeField] private Button _LoadButton;
    [SerializeField] private AudioSource _PlayAudio;

    private int mRowSize;
    private int mColumnSize;

    void Start()
    {
        _StartButton.interactable = false;
        _LoadButton.interactable = SaveManager._Instance != null && SaveManager._Instance.HasSaveFile();
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
        SaveManager._Instance.DeleteSaveFile(); 
        CardsManager._Instance.OnStartGame(mRowSize, mColumnSize);
        StartCoroutine(DisableAfterSound());
    }

    public void OnLoadButtonClick()
    {
        if (SaveManager._Instance != null && SaveManager._Instance.HasSaveFile())
        {
            SaveData data = SaveManager._Instance.LoadGame();
            if (data != null)
            {
                _PlayAudio.Play();
                CardsManager._Instance.OnStartGame(data.gridRows, data.gridColumns);
                StartCoroutine(DisableAfterSound());
            }
        }
    }

    IEnumerator DisableAfterSound()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void CheckGridSize()
    {
        if (mRowSize > 0 && mColumnSize > 0 && mRowSize * mColumnSize % 2 == 0 && mRowSize * mColumnSize < 88)
            _StartButton.interactable = true;
    }
}
