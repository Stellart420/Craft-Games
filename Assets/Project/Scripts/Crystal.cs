using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crystal : MonoBehaviour
{
    [SerializeField] int hasScore = 1;

    UnityAction CollisionAction;

    public void Init(UnityAction col_action)
    {
        Init(hasScore, col_action);
    }

    public void Init(int score, UnityAction col_action)
    {
        hasScore = score;
        CollisionAction = null;
        CollisionAction += col_action;
    }

    
    public void Reset()
    {
        gameObject.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CollisionAction?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
