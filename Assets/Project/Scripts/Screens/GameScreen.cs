using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    [SerializeField] Button controlTurnBtn;
    [SerializeField] Text score;

    void Start()
    {
        controlTurnBtn.onClick.AddListener(() => UIController.Instance.ControlBtnPress());
        UIController.Instance.ChangeScoreAction += SetScore;
    }

    void SetScore(int _score)
    {
        score.text = $"{_score}";
    }
}
