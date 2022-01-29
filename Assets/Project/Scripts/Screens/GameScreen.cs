using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    [SerializeField] Button controlTurnBtn;

    void Start()
    {
        controlTurnBtn.onClick.AddListener(() => UIController.Instance.ControlBtnPress());
    }
}
