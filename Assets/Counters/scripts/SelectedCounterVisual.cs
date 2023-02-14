using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter clearCounters;
    [SerializeField] GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == clearCounters) {
            Show();
        } else
        {
            Hide();
        }
    }

    void Show()
    {
        visualGameObject.SetActive(true);
    }
    void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
