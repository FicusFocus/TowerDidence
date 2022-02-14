using IJunior.TypedScenes;
using UnityEngine;


 public class Level1Entry : MonoBehaviour, ISceneLoadHandler<AudiolConfig>
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private EnemySpawner _spavner;
    [SerializeField] private BuildController _buildController;

    public void OnSceneLoaded(AudiolConfig config)
    {
        _backgroundMusic.volume = config.Music;
        _spavner.SetEnemyAudioSourceValue(config.Sound);
        _buildController.SetTowerAudioSourceStartValue(config.Sound);
    }
}
