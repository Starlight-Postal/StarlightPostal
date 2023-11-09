using System.Collections;
using System.Collections.Generic;
using IngameDebugConsole;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static void GoToGameOver()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public static void GameOverFromPop()
    {
        GoToGameOver();
    }

    [ConsoleMethod("kill", "kills you instantly")]
    public static void KillPlayerCommand()
    {
        FindObjectOfType<SaveFileManager>().saveData.lives = 0;
        GoToGameOver();
    }
}
