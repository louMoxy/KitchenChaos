using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfullReciped().ToString();
        }
        else
        {
            Hide();
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    void Show()
    {
        gameObject.SetActive(true);
    }
}
