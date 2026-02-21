using UnityEngine;
using UnityEngine.UI;

public class SaveGameHandler : MonoBehaviour
{
    [SerializeField] private Button _SaveButton;

    private CardsHandler mCardsHandler;
    private bool mShouldRestoreGame = false;

    void Start()
    {
        mCardsHandler = FindObjectOfType<CardsHandler>();

        if (_SaveButton != null)
            _SaveButton.onClick.AddListener(OnSaveClicked);

        if (SaveManager._Instance != null && SaveManager._Instance.HasSaveFile())
        {
            SaveData data = SaveManager._Instance.LoadGame();
            if (data != null)
            {
                SavedCardState[] cardStates = data.GetCardStates();
                if (cardStates != null && cardStates.Length > 0)
                    mShouldRestoreGame = true;
            }
        }

        CardsManager.cardsReady += OnCardsReady;
    }

    void OnDestroy()
    {
        if (_SaveButton != null)
            _SaveButton.onClick.RemoveListener(OnSaveClicked);
        
        CardsManager.cardsReady -= OnCardsReady;
    }

    private void OnCardsReady()
    {
        if (mShouldRestoreGame && mCardsHandler != null)
        {
            RestoreLoadedGame();
            mShouldRestoreGame = false;
        }
    }

    private void OnSaveClicked()
    {
        if (mCardsHandler == null)
            return;

        SavedCardState[] cardStates = mCardsHandler.GetCardStates();
        int score = 0;
        int turns = 0;
        int rows = 0;
        int columns = 0;

        HUDHandler hudHandler = FindObjectOfType<HUDHandler>();
        if (hudHandler != null)
        {
            score = hudHandler.GetScore();
            turns = hudHandler.GetTurns();
        }

        mCardsHandler.GetGridDimensions(out rows, out columns);
        SaveManager._Instance.SaveGameSnapshot(score, turns, rows, columns, cardStates);
    }

    private void RestoreLoadedGame()
    {
        if (mCardsHandler == null)
            return;

        SaveData data = SaveManager._Instance.GetCurrentSaveData();
        if (data == null)
            return;
        
        SavedCardState[] cardStates = data.GetCardStates();
        if (cardStates == null || cardStates.Length == 0)
            return;

        mCardsHandler.RestoreFromSave(data);
        
        HUDHandler hudHandler = FindObjectOfType<HUDHandler>();
        if (hudHandler != null)
            hudHandler.RestoreProgress(data.score, data.turns);
    }

    private int GetMatchedPairCount(SavedCardState[] states)
    {
        if (states == null) return 0;
        int count = 0;
        foreach (SavedCardState state in states)
        {
            if (state.isMatched) count++;
        }
        return count / 2;
    }
}
