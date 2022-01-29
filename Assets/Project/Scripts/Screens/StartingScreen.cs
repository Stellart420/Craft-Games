using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingScreen : Screen
{
    [SerializeField] Button startBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(() => UIController.Instance.StartGame());
    }

    internal override void Hide()
    {
        base.Hide();
    }
}
