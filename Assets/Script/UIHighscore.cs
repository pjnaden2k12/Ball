using UnityEngine;
using TMPro;

public class UIHighScore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI highScoreText; // Text hiển thị điểm cao

    private void Start()
    {
        UpdateHighScoreUI();
    }

    public void UpdateHighScoreUI()
    {
        // Lấy điểm cao nhất đã lưu từ PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        // Hiển thị điểm cao trong UI
        highScoreText.text = "" + highScore;
    }

    public void ClearHighScore()
    {
        // Xóa điểm cao
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();

        // Cập nhật lại UI sau khi xóa điểm cao
        UpdateHighScoreUI();
    }
}