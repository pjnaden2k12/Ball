using UnityEngine;
using TMPro;

public class UIHighScore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI highScoreText;

    private void Start()
    {
        UpdateHighScoreUI();
    }

    public void UpdateHighScoreUI()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "" + highScore;
    }

    public void ClearHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
        UpdateHighScoreUI();
    }
}