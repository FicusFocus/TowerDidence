using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class EndGameScreen : MonoBehaviour
{
    [SerializeField] private Button _goToMainMenuButton, _restartButton;

    public event UnityAction RestartButtonClicked;
    public event UnityAction MenuButtonClicked;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnrestartButtonClick);
        _goToMainMenuButton.onClick.AddListener(OnMenuButtonClic);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnrestartButtonClick);
        _goToMainMenuButton.onClick.RemoveListener(OnMenuButtonClic);
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
