using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaceLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _choceLevel;

    [SerializeField] protected AudiolConfig LevelConfig;

    private void OnEnable()
    {
        _choceLevel.onClick.AddListener(OnChoiseLevelClick);
    }

    private void OnDisable()
    {
        _choceLevel.onClick.RemoveListener(OnChoiseLevelClick);
    }

    protected abstract void OnChoiseLevelClick();
}
