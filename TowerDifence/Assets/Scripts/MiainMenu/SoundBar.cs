using UnityEngine.Events;

public class SoundBar : Bar
{
    public event UnityAction<float> ValueChanged;

    protected override void OnValueChanged(float value)
    {
        ValueChanged?.Invoke(value);
        AudioConfigurations.InitSound(value);
    }
}
