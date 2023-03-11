using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }


    [SerializeField] Button soundEffectsButton;
    [SerializeField] Button MusicButton;
    [SerializeField] Button CloseButton;
    [SerializeField] Button MoveUpButton;
    [SerializeField] Button MoveDownButton;
    [SerializeField] Button MoveRightButton;
    [SerializeField] Button MoveLeftButton;
    [SerializeField] Button InteractButton;
    [SerializeField] Button InteractAlternateButton;
    [SerializeField] Button PauseButton;
    [SerializeField] TextMeshProUGUI soundEffectText;
    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] TextMeshProUGUI MoveUpText;
    [SerializeField] TextMeshProUGUI MoveDownText;
    [SerializeField] TextMeshProUGUI MoveLeftText;
    [SerializeField] TextMeshProUGUI MoveRightText;
    [SerializeField] TextMeshProUGUI InteractText;
    [SerializeField] TextMeshProUGUI InteractAlternateText;
    [SerializeField] TextMeshProUGUI PauseText;
    [SerializeField] Transform pressToRebindKeyTransform;

    private Action OnCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        Hide();
        HidePressToRebindKey();

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        MusicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        CloseButton.onClick.AddListener(() => {
            Hide();
            if (OnCloseButtonAction != null)
            {
                OnCloseButtonAction();
            }
        });

        MoveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        MoveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        MoveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        MoveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        InteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        InteractAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact_Alternate); });
        PauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        UpdateVisual();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();

        MoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        MoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        MoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        MoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        InteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

    public void Show(Action OnCloseButtonAction)
    {
        this.OnCloseButtonAction = OnCloseButtonAction;
        gameObject.SetActive(true);
    }   
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }
}
