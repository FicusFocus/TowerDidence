using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint, _container;
    [SerializeField] private Player _player;
    [SerializeField] private float _delay;

    private Wave _currentWave;
    private int _enemyesAmountInWave, _totalEnemyesAmount, _enemyDiedCount, _currentWaveNumber, _alreadySpawned;
    private float _timeAfterlastSpawn, _delayBetweenNextWaves, _enemyAudioSourceStartValue;
    private bool _canSetNewWawe, _waveFinished, _allWavesFinnished;

    public event UnityAction<Enemy> EnemySpawned;
    public event UnityAction AllWavesFinnished;

    private void Awake()
    {
        _currentWaveNumber = 0;
        _alreadySpawned = 0;
        _timeAfterlastSpawn = 0;
        _delayBetweenNextWaves = 0;
        _enemyAudioSourceStartValue = 1;
        _totalEnemyesAmount = 0;
        _allWavesFinnished = false;
    }

    private void Start()
    {
        SetWave(_currentWaveNumber);
        _canSetNewWawe = false;
        _waveFinished = false;
    }

    private void Update()
    {
        if (_allWavesFinnished)
            return;

        if (_currentWave == null)
        {
            _delayBetweenNextWaves += Time.deltaTime;

            if (_currentWaveNumber >= _waves.Count && _enemyDiedCount == _totalEnemyesAmount)
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
            _waveFinished = true;
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

        _totalEnemyesAmount += _enemyesAmountInWave;
    }

    private int GetRandomenemy()
    {
        return Random.Range(0, _currentWave.EnemyType.Count);
    }

    private void SpawnEnemy(Enemy tamplate)
    {
        var newEnemy = Instantiate(tamplate, _spawnPoint.position, Quaternion.identity, _container);
        EnemySpawned?.Invoke(newEnemy);
        newEnemy.Died += OnEnemyDied;
        newEnemy.SetAudioSourseVolumeValue(_enemyAudioSourceStartValue);
        _alreadySpawned++;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _player.AddMoney(enemy.Revard);
        _enemyDiedCount++;
        enemy.Died -= OnEnemyDied;
    }

    public void SetEnemyAudioSourceValue(float value)
    {
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
