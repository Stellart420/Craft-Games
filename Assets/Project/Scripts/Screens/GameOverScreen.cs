using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    [SerializeField] Button restartBtn;

    void Start()
    {
        restartBtn.onClick.AddListener(() => UIController.Instance.RestartGame());
    }
}
