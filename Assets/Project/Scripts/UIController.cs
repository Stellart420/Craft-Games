using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] StartingScreen startingScreen;
    [SerializeField] GameScreen gameScreen;
    [SerializeField] GameOverScreen gameOverScreen;
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    private void Start()
    {
        
    }

    public void StartGame()
    {
        startingScreen.Hide();
        gameScreen.Show();
        GameController.Instance.StartGame();
    }
    
    public void RestartGame()
    {
        gameOverScreen.Hide();
        GameController.Instance.RestartGame();
        startingScreen.Show();
    }

    public void ControlBtnPress()
    {
        PlayerController.Instance.ChangeTurn();
    }

    public void GameOver()
    {
        gameScreen.Hide();
        gameOverScreen.Show();
    }

}
