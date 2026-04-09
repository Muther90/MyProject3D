using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class NotificationScreen : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    public event Action ButtonClicked;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(ButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(ButtonClick);
    }

    private void Start()
    {
        Close();
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void ButtonClick()
    {
        Close();
        ButtonClicked?.Invoke();
    }
}