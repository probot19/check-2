using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager _Instance { get; private set; }

    private const string SAVE_KEY = "GameSaveData";
    private SaveData mCurrentSaveData;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(this);
        else
            _Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SaveGameSnapshot(int score, int turns, int rows, int columns, SavedCardState[] cardStates)
    {
        if (cardStates == null)
            return;

        mCurrentSaveData = new SaveData(score, turns, rows, columns, cardStates);
        string json = JsonUtility.ToJson(mCurrentSaveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public SaveData LoadGame()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return null;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        mCurrentSaveData = JsonUtility.FromJson<SaveData>(json);
        return mCurrentSaveData;
    }

    public SaveData GetCurrentSaveData()
    {
        return mCurrentSaveData;
    }

    public bool HasSaveFile()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }

    public void DeleteSaveFile()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        mCurrentSaveData = null;
    }
}
