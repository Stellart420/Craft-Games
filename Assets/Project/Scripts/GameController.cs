using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    bool isStarted;

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
        isStarted = false;
        PlatformGenerator.Instance.Reset();
        PlayerController.Instance.Reset();
    }

    public void GameOver()
    {
        UIController.Instance.GameOver();
    }
}
