using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    public event UnityAction SettingsClosed;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnClosedButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnClosedButtonClick);
    }

    private void OnClosedButtonClick()
    {
        SettingsClosed?.Invoke();
        gameObject.SetActive(false);
    }
}
