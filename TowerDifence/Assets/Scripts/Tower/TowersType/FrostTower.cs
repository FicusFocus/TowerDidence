using System.Collections.Generic;
using UnityEngine;

public class FrostTower : Tower
{
    [SerializeField, Range(0, 1)] private float _frostPower;
    [SerializeField] private Color _freezColor;

    private List<Enemy> _frozenEnemyes = new List<Enemy>();
    private List<Enemy> _targets = new List<Enemy>();

    private void FixedUpdate()
    {
        FindEnemyes();

        if (_targets.Count > 0)
            TryToAddInFrozenEnemyes();

        _targets.Clear();
    }

    private void FindEnemyes()
    {
        var hitList = Physics2D.OverlapCircleAll(transform.position, SearchArea);

        foreach (var collider in hitList)
        {
            if (collider.TryGetComponent(out Enemy enemy))
                _targets.Add(enemy);
        }
    }

    private void TryToAddInFrozenEnemyes()
    {
        foreach (var newTarget in _targets)
        {
            bool alreadyFrozen = false;

            foreach (var enemy in _frozenEnemyes)
            {
                if (enemy == newTarget)
                    alreadyFrozen = true;
            }

            if (alreadyFrozen)
                continue;

            FreezTraget(newTarget);
            _frozenEnemyes.Add(newTarget);
        }
    }

    private void FreezTraget(Enemy target)
    {
        var targetmoveSpeed = target.CurrentMoveSpeed;
        var newMoveSpeed = targetmoveSpeed - (targetmoveSpeed * _frostPower);

        target.SetMovespeed(newMoveSpeed);
        target.SetNewColor(_freezColor);
    }
}
