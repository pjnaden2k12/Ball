using UnityEngine;

public static class Highscore
{
    private const string HIGH_SCORE_KEY = "HighScore";

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    public static void SaveHighScore(int score)
    {
        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(HIGH_SCORE_KEY);
    }
}