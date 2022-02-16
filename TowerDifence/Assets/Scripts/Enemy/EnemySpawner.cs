using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _container;
    [SerializeField] private float _delay;

    private Wave _currentWave;
    private int _enemyesAmountInWave;
    private int _currentWaveNumber = 0;
    private int _alreadySpawned;
    private float _timeAfterlastSpawn = 0;
    private float _delayBetweenNextWaves = 0;
    private float _enemyAudioSourceStartValue = 1;
    private bool _canSetNewWawe = false;
    private bool _allWavesFinnished = false;

    public event UnityAction<Enemy> EnemySpawned;
    public event UnityAction AllWavesFinnished;

    private void Start()
    {
        SetWave(_currentWaveNumber);
        _canSetNewWawe = false;
    }

    private void Update()
    {
        if (_allWavesFinnished)
            return;

        if (_currentWave == null)
        {
            _delayBetweenNextWaves += Time.deltaTime;

            if (_currentWaveNumber >= _waves.Count)
            {
                AllWavesFinnished?.Invoke();
                _allWavesFinnished = true;
            }

            if (_delayBetweenNextWaves >= _delay)
                _canSetNewWawe = true;
            else
                _canSetNewWawe = false;

            if (_canSetNewWawe)
            {
                _currentWaveNumber++;
                SetWave(_currentWaveNumber);
            }

            return;
        }

        _timeAfterlastSpawn += Time.deltaTime;

        if (_timeAfterlastSpawn >= _currentWave.Delay)
        {
            int randomEnemyType = GetRandomenemy();

            if (_currentWave.EnemyType[randomEnemyType].Amount > 0)
            {
                SpawnEnemy(_currentWave.EnemyType[randomEnemyType].Tamplate);
                _currentWave.EnemyType[randomEnemyType].Amount--;
                _timeAfterlastSpawn = 0;
            }
        }

        if (_alreadySpawned == _enemyesAmountInWave)
        {
            _alreadySpawned = 0;
            _currentWave = null;
        }
    }

    private void SetWave(int index)
    {
        if (index >= _waves.Count)
            return;

        _enemyesAmountInWave = 0;
        _currentWave = _waves[index];

        for (int i = 0; i < _currentWave.EnemyType.Count; i++)
            _enemyesAmountInWave += _currentWave.EnemyType[i].Amount;
    }

    private int GetRandomenemy()
    {
        return Random.Range(0, _currentWave.EnemyType.Count);
    }

    private void SpawnEnemy(Enemy tamplate)
    {
        var newEnemy = Instantiate(tamplate, _spawnPoint.position, Quaternion.identity, _container);
        EnemySpawned?.Invoke(newEnemy);
        newEnemy.SetAudioSourseVolumeValue(_enemyAudioSourceStartValue);
        _alreadySpawned+=1;
    }

    public void SetEnemyAudioSourceValue(float value)
    {
        Debug.Log(value);
        _enemyAudioSourceStartValue = value;
    }
}

[System.Serializable]
class Wave
{
    public List<EnemyTamplate> EnemyType;
    public float Delay;
}

[System.Serializable]
class EnemyTamplate
{
    public int Amount;
    public Enemy Tamplate;
}
