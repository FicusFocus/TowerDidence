using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    [SerializeField] private List<Transform> _placesPosition;
    [SerializeField] private PlaceForBuild _template;
    [SerializeField] private Transform _towersContainer; 
    [SerializeField] private Shop _shop;

    private PlaceForBuild[] _places;
    private PlaceForBuild _chosenPlace;
    private float _towerAudioSourceStartValue = 1;

    private void OnEnable()
    {
        _shop.TowerBuyed += OnTowerBuyed;
    }

    private void OnDisable()
    {
        _shop.TowerBuyed -= OnTowerBuyed;
    }

    private void Start()
    {
        InstantiateBuildPlaces();
        SetShopPanelActive(false);
    }

    private void SetShopPanelActive(bool isActive)
    {
        _shop.IsActive(isActive);
    }

    private void InstantiateBuildPlaces()
    {
        foreach (var place in _placesPosition)
        {
            var newBildingPLace = Instantiate(_template, place.position, Quaternion.identity, place);
            newBildingPLace.DontActive += OnPlaceDontActive;
            newBildingPLace.PlaceChoosing += OnAnyPlaceChoosed;
        }

        PlaceForBuild[] places = GetComponentsInChildren<PlaceForBuild>();
        _places = places;
    }

    private void OnPlaceDontActive()
    {
        SetShopPanelActive(false);
    }

    private void OnAnyPlaceChoosed(PlaceForBuild buildingPLace)
    {
        for (int i = 0; i < _places.Length; i++)
        {
            if (_places[i] == buildingPLace)
            {
                SetShopPanelActive(true);
                _chosenPlace = buildingPLace;
                continue;
            }

            _places[i].Reset();
        }
    }

    private void OnTowerBuyed(Tower tower)
    {
        if (_chosenPlace == null)
            return;

        BuildTower(_chosenPlace, tower);
        _chosenPlace.DontActive -= OnPlaceDontActive;
        _chosenPlace.PlaceChoosing -= OnAnyPlaceChoosed;
    }

    private void BuildTower(PlaceForBuild place, Tower tower)
    {
        var newTower = Instantiate(tower, place.transform.position, Quaternion.identity, _towersContainer);
        newTower.SetAudioSourceValue(_towerAudioSourceStartValue);
        place.gameObject.SetActive(false);
    }

    public void SetTowerAudioSourceStartValue(float value)
    {
        _towerAudioSourceStartValue = value;
    }
}
