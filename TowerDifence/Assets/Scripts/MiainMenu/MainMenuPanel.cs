using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button _startButton, _exitButton, _settingsButton;
    [SerializeField] private LevelChocer _levelPanel;
    [SerializeField] private Settings _settings;

    private void OnEnable()
    {
        _levelPanel.LevelPanelClosed += OnLevelPanelClosed;
        _settings.SettingsClosed += OnSettingsClosed;
        _startButton.onClick.AddListener(OnStartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
    }

    private void OnDisable()
    {
        _levelPanel.LevelPanelClosed -= OnLevelPanelClosed;
        _settings.SettingsClosed -= OnSettingsClosed;
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
    }

    private void Start()
    {
        _levelPanel.gameObject.SetActive(false);
        _settings.gameObject.SetActive(false);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnSettingsButtonClick()
    {
        _settings.gameObject.SetActive(true);
        SetButtonsState(false);
    }

    private void OnStartButtonClick()
    {
        _levelPanel.gameObject.SetActive(true);
        SetButtonsState(false);
    }

    private void OnSettingsClosed()
    {
        _settings.gameObject.SetActive(false);
        SetButtonsState(true);
    }

    private void OnLevelPanelClosed()
    {
        _levelPanel.gameObject.SetActive(false);
        SetButtonsState(true);
    }

    private void SetButtonsState(bool state)
    {
        _startButton.gameObject.SetActive(state);
        _exitButton.gameObject.SetActive(state);
        _settingsButton.gameObject.SetActive(state);
    }
}
