using UnityEngine;

public class MainMenuAudiioController : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource, _soundAudioSource;
    [SerializeField] private AudioClip music, _demoSound;
    [SerializeField] private MusicBar _musicBar;
    [SerializeField] private SoundBar _soundBar;
    [SerializeField] private AudiolConfig _levelConfig;

    private void OnEnable()
    {
        _musicBar.ValueChanged += SetMusicValue;
        _soundBar.ValueChanged += SetSoundValue;
    }

    private void OnDisable()
    {
        _musicBar.ValueChanged -= SetMusicValue;
        _soundBar.ValueChanged -= SetSoundValue;
    }

    private void Start()
    {
        _levelConfig.InitMusic(_musicAudioSource.volume);
        _levelConfig.InitSound(_soundAudioSource.volume);
        _soundAudioSource.clip = _demoSound;
        _musicAudioSource.clip = music;
        _musicAudioSource.Play();
    }

    private void SetMusicValue(float value)
    {
        _musicAudioSource.volume = value;
    }

    private void SetSoundValue(float value)
    {
        _soundAudioSource.volume = value;
        _soundAudioSource.Play();
    }
}
