using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coundownText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        } else
        {
            Hide();
        }
    }

    private void Update()
    {
        coundownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownTime()).ToString();
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
