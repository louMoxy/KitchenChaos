using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI keymoveupText;
    [SerializeField] TextMeshProUGUI keymoveDownText;
    [SerializeField] TextMeshProUGUI keymoveLeftText;
    [SerializeField] TextMeshProUGUI keymoveRightText;
    [SerializeField] TextMeshProUGUI keyInteractText;
    [SerializeField] TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] TextMeshProUGUI keyPauseText;

    private void Start()
    {
        UpdateVisual();

        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;


        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        keymoveupText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keymoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keymoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keymoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }


    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
