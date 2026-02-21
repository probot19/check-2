using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private Canvas _GameOverCanvas;
    [SerializeField] private Button _RestartButton;
    [SerializeField] private Button _QuitButton;
    [SerializeField] private AudioSource _AudioSource;

    void Awake()
    {
        GameOverManager.gameOver += OnGameOver;
    }

    void Start()
    {
        _GameOverCanvas.gameObject.SetActive(false);
        _RestartButton.onClick.AddListener(OnRestartClicked);
        _QuitButton.onClick.AddListener(OnQuitClicked);
    }

    void OnDestroy()
    {
        GameOverManager.gameOver -= OnGameOver;
        _RestartButton.onClick.RemoveListener(OnRestartClicked);
        _QuitButton.onClick.RemoveListener(OnQuitClicked);
    }

    private void OnGameOver(int score, int turns, int matchedCards)
    {
        _AudioSource.Play();
        _GameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnRestartClicked()
    {
        Time.timeScale = 1f;
        SaveManager._Instance.DeleteSaveFile();
        SceneManager.LoadScene("Game");
    }

    private void OnQuitClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
