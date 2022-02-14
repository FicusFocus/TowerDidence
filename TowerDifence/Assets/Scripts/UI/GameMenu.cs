using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _menu, _play, _replay, _exit;

    public event UnityAction AnyPanelOpened;
    public event UnityAction MenuClosed;

    private void Awake()
    {
        _menuPanel.SetActive(false);
    }

    private void OnEnable()
    {
        _menu.onClick.AddListener(OnMenuButtonClick);
        _play.onClick.AddListener(OnPlayButtonClick);
        _replay.onClick.AddListener(OnReplayButtonClik);
        _exit.onClick.AddListener(OnExitButtonClic);
    }

    private void OnDisable()
    {
        _menu.onClick.RemoveListener(OnMenuButtonClick);
        _play.onClick.RemoveListener(OnPlayButtonClick);
        _replay.onClick.RemoveListener(OnReplayButtonClik);
        _exit.onClick.RemoveListener(OnExitButtonClic);
    }

    private void OnExitButtonClic()
    {
        IJunior.TypedScenes.MainMenu.Load();
    }

    private void OnReplayButtonClik()
    {
        _menuPanel.SetActive(false);
        _menu.enabled = true;
        MenuClosed?.Invoke();
        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void OnPlayButtonClick()
    {
        _menuPanel.SetActive(false);
        _menu.gameObject.SetActive(true);
        MenuClosed?.Invoke();
    }

    private void OnMenuButtonClick()
    {
        _menuPanel.SetActive(true);
        _menu.gameObject.SetActive(false);
        AnyPanelOpened?.Invoke();
    }
}
