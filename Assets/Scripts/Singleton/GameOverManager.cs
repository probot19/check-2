using System;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager _Instance { get; private set; }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
            Destroy(this);
        else
            _Instance = this;
    }

    public static Action<int, int, int> gameOver = null; 

    public void OnGameOver(int score, int turns, int matchedCards)
    {
        if (gameOver != null)
            gameOver.Invoke(score, turns, matchedCards);
    }

    public static Action restartGame = null;

    public void OnRestartGame()
    {
        if (restartGame != null)
            restartGame.Invoke();
    }

    public static Action resumeGame = null;

    public void OnResumeGame()
    {
        if (resumeGame != null)
            resumeGame.Invoke();
    }

    public static Action quitToMenu = null;

    public void OnQuitToMenu()
    {
        if (quitToMenu != null)
            quitToMenu.Invoke();
    }
}
