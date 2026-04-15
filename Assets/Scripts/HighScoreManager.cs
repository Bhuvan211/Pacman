using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private const string HIGH_SCORE_KEY = "PacManHighScore";

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    public static void SaveScore(int score)
    {
        int highScore = GetHighScore();
        if (score > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
            PlayerPrefs.Save();
            Debug.Log($"New high score: {score}!");
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(HIGH_SCORE_KEY);
        PlayerPrefs.Save();
        Debug.Log("High score reset!");
    }

    public static string FormatScore(int score)
    {
        return $"Score: {score}";
    }
}
