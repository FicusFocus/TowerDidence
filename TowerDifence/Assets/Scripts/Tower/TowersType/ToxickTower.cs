using System.Collections.Generic;
using UnityEngine;

public class ToxickTower : Tower
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private Color _poisoningColor;

    private float _lastAttackTime = 0;
    private List<Enemy> _poisoningTargets = new List<Enemy>();
    private List<Enemy> _targets = new List<Enemy>();

    private void Update()
    {
        _lastAttackTime += Time.deltaTime;

        if (_lastAttackTime >= _delay)
        {
            TryFindTargets();

            if (_targets.Count > 0)
            {
                foreach (var target in _targets)
                    TryToAddInPoisoningTargets(target);

                _targets.Clear();
                ClearPoisoningTargetsList();
            }

            _lastAttackTime = 0;
        }
    }

    private void TryToAddInPoisoningTargets(Enemy target)
    {
        if (_poisoningTargets.Count == 0)
        {
            _poisoningTargets.Add(target);
            PoisonTarget(target);
            return;
        }

        bool alreadyPoison = false;

        foreach (var poisoningTarget in _poisoningTargets)
        {
            if (poisoningTarget == target)
                alreadyPoison = true;
        }

        if (alreadyPoison == false)
        {
            _poisoningTargets.Add(target);
            PoisonTarget(target);
        }
    }

    private void PoisonTarget(Enemy target)
    {
        target.SetNewColor(_poisoningColor);
        target.TakeDamage(_damage);
    }

    private void TryFindTargets()
    {
        var targetList = new List<Enemy>();
        var hitList = Physics2D.OverlapCircleAll(transform.position, SearchArea);

        foreach (var collider in hitList)
        {

            if (collider.TryGetComponent(out Enemy enemy))
                targetList.Add(enemy);
        }

        _targets = targetList;
    }

    private void ClearPoisoningTargetsList()
    {
        foreach (var item in _poisoningTargets)
            item.SetNewColor(item.BaceColor);

        _poisoningTargets.Clear();
    }
}
