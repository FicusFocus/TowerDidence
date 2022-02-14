using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudiolConfig _audioConfigurations;

    public event UnityAction<float> ValueChanged;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        ValueChanged?.Invoke(value);
        _audioConfigurations.InitMusic(value);
    }
}
