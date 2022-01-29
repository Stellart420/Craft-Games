using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] bool isStartActivated;

    internal CanvasGroup panel;

    private void Awake()
    {
        panel = GetComponent<CanvasGroup>();
        if (isStartActivated)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    internal virtual void Show()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        panel.alpha = 1f;
    }

    internal virtual void Hide()
    {
        panel.alpha = 0f;
        gameObject.SetActive(false);
    }

}
