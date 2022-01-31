using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

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
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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
