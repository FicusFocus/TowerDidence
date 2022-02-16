using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private KillZone _killZone;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _startMoney;
    [SerializeField] private int _healthPoint;

    private int _money;
    private int _currentHealthPoints;
    private int _diedEnemyCount;
    private int _totalEnemyCount = 0;

    public int Money => _money;

    public event UnityAction AllEnemyesPassed;
    public event UnityAction AllEnemyesKilled;

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
            AllEnemyesPassed?.Invoke();
    }

    private void AddMoney(int revard)
    {
        if (revard <= 0)
            return;

        _money += revard;
        ChangeMoneyText();
    }

    private void ChangeMoneyText()
    {
        _text.text = _money.ToString();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        AddMoney(enemy.Revard);
        _diedEnemyCount += 1;

        if (_totalEnemyCount == _diedEnemyCount)
            AllEnemyesKilled?.Invoke();

        enemy.Died -= OnEnemyDied;
    }

    public void NewEnemyFound(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
        _totalEnemyCount++;
    }

    public bool BuyTower(Tower tower)
    {
        if (tower.Price <= _money)
        {
            _money -= tower.Price;
            ChangeMoneyText();
            return true;
        }

        return false;
    }
}
