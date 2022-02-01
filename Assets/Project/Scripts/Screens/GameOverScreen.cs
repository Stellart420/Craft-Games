using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    [SerializeField] Button restartBtn;
    [SerializeField] Text bestScore;
    PlayerPrefs bestScorePrefs;
    void Start()
    {
        restartBtn.onClick.AddListener(() => UIController.Instance.RestartGame());
    }

    internal override void Show()
    {
        var best_score = PlayerPrefs.GetInt("BestScore", 0);
        bestScore.text = $"{best_score}";
        base.Show();
        if (GameController.Instance.Score > best_score)
        {
            bestScore.text = $"{GameController.Instance.Score}";
            PlayerPrefs.SetInt("BestScore", GameController.Instance.Score);
        }
    }
}
