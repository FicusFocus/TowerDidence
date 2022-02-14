using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreenPanel;
    [SerializeField] private Button _restartButton, _menuButton;

    public event UnityAction RestartButtonClicked;
    public event UnityAction MenuButtonClicked;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnrestartButtonClick);
        _menuButton.onClick.AddListener(OnMenuButtonClic);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnrestartButtonClick);
        _menuButton.onClick.RemoveListener(OnMenuButtonClic);
    }

    private void OnMenuButtonClic()
    {
        MenuButtonClicked?.Invoke();
    }

    private void OnrestartButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}