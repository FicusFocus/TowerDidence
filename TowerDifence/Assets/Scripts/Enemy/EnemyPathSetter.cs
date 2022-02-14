using System.Collections.Generic;
using UnityEngine;

public class EnemyPathSetter : MonoBehaviour
{
    [SerializeField] private List<Transform> _path;
    [SerializeField] private EnemySpawner _spawner;
    
    private Enemy _enemy;

    private void OnEnable()
    {
        _spawner.EnemySpawned += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _spawner.EnemySpawned -= OnEnemySpawned;
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.InitPath(_path);
    }
}
