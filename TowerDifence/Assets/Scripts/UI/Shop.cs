using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Tower> _towers;
    [SerializeField] private TowerView _towerView;
    [SerializeField] private Transform _conteiner;
    [SerializeField] private Player _player;

    public event UnityAction<Tower> TowerBuyed;

    private void Start()
    {
        foreach (var tower in _towers)
        {
            AddTowerView(tower);
        }
    }

    private void AddTowerView(Tower tower)
    {
        var view = Instantiate(_towerView, _conteiner);
        view.Renderer(tower);
        view.SellButtonClick += OnSellButtonClick;
    }

    private void OnSellButtonClick(Tower tower, TowerView towerView)
    {
        if (tower == null || towerView == null)
            return;
        TrySellTower(tower, towerView);
    }

    private void TrySellTower(Tower tower, TowerView towerView)
    {

        if (tower.Price <= _player.Money)
        {
            _player.BuyTower(tower);
            TowerBuyed?.Invoke(tower);
            IsActive(false);
        }
    }

    public void IsActive(bool state)
    {
        gameObject.SetActive(state);
    }
}
