using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] GameObject hasProgressGameObject;
    
    IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError($"GameObject { hasProgressGameObject } does not have a component that implements IHasProgress");
        }

        barImage.fillAmount = 0f;
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnPublicChangesEventsArgs e)
    {
        barImage.fillAmount = e.progressNormalised;

       if(e.progressNormalised == 0f || e.progressNormalised == 1f)
        {
            Hide();
        } else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
