using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _startMoney, _healthPoint;
    [SerializeField] private KillZone _killZone;

    private int _money, _currentHealthPoints;
    private List<Tower> _towers = new List<Tower>();

    public int Money => _money;

    public event UnityAction AllEnemyesPassed;

    private void OnEnable()
    {
        _killZone.EnemyPassed += OnEnemyPassed;
    }

    private void OnDisable()
    {
        _killZone.EnemyPassed -= OnEnemyPassed;
    }

    private void Start()
    {
        _currentHealthPoints = _healthPoint;
        _money = _startMoney;
        _text.text = _money.ToString();
    }

    private void OnEnemyPassed()
    {
        _currentHealthPoints--;

        if (_currentHealthPoints <= 0)
        {
            AllEnemyesPassed?.Invoke();
        }
    }

    public bool BuyTower(Tower tower)
    {
        if (tower.Price <= _money)
        {
            _money -= tower.Price;
            _text.text = _money.ToString();
            _towers.Add(tower);
            return true;
        }

        return false;
    }

    public void AddMoney(int revard)
    {
        if (revard <= 0)
            return;

        _money += revard;
        _text.text = _money.ToString();
    }
}
