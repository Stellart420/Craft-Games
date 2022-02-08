using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController Instance;
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    [SerializeField] LevelDifficult difficult = LevelDifficult.Easy;

    public LevelDifficult Difficult => difficult;

    public UnityAction RestartAction;

    bool isStarted;

    int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UIController.Instance.SetScore(score);
        }
    }    

    public bool IsStarted => isStarted;
   

    public void StartGame()
    {
        isStarted = true;
    }

    public void RestartGame()
    {
        RestartAction();
        isStarted = false;
        Score = 0;
    }

    public void GameOver()
    {
        UIController.Instance.GameOver();
    }

    public void AddScore(int _added_score)
    {
        Score = score + _added_score;
    }
}
public enum LevelDifficult
{
    Easy,
    Normal,
    Hard,
}
