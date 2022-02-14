using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelChocer : MonoBehaviour
{
    [SerializeField] private Button _closedButton;
    [SerializeField] private Transform _levelViewConteiner;
    [SerializeField] private List<BaceLevel> _levels;

    public event UnityAction LevelPanelClosed;

    private void OnEnable()
    {
        _closedButton.onClick.AddListener(OnClosedButtonClick);
    }

    private void OnDisable()
    {
        _closedButton.onClick.RemoveListener(OnClosedButtonClick);
    }

    private void Start()
    {
        foreach (var level in _levels)
            Instantiate(level, _levelViewConteiner);
    }

    private void OnClosedButtonClick()
    {
        LevelPanelClosed?.Invoke();
        gameObject.SetActive(false);
    }
}
