using UnityEngine.Events;

public class MusicBar : Bar
{
    public event UnityAction<float> ValueChanged;
    protected override void OnValueChanged(float value)
    {
        ValueChanged?.Invoke(value);
        AudioConfigurations.InitMusic(value);
    }
}
