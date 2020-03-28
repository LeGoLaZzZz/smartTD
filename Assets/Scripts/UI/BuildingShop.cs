using System;
using System.Collections.Generic;
using Buildings;
using DefaultNamespace;
using Infos;
using Spawners;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BuildingShop : MonoBehaviour
{
    [SerializeField] private BuildingShopButton shopButtonPrefab;
    [SerializeField] private Transform layoutGroupTransform;

    private List<BuildingShopButton> _buttons = new List<BuildingShopButton>();
    private BuildingShopButton _currentShopButton;

    private static BuildingShop _instance;
    public static BuildingShop GetInstance() => _instance;

    private void Awake()
    {
        _instance = this;
    }


    public void OnShopButtonClicked(BuildingShopButton button)
    {
        if (_currentShopButton != null)
        {
            OnSpawningCancel();
        }

        if (IsGoldEnough(button))
        {
            BluePrintTuner.GetInstance().SetBluePrint(button.buildingType);
            _currentShopButton = button;
            BluePrintTuner.GetInstance().BuildingSpawned += OnBuildingSpawned;
        }
    }


    public void OnSpawningCancel()
    {
        _currentShopButton = null;
        BluePrintTuner.GetInstance().BuildingSpawned -= OnBuildingSpawned;
    }

    public void OnBuildingSpawned()
    {
        GoldManager.GetInstance().TryTakeGold(_currentShopButton.currentPrice);
        _currentShopButton = null;
        BluePrintTuner.GetInstance().BuildingSpawned -= OnBuildingSpawned;
    }

    public bool IsGoldEnough(BuildingShopButton button)
    {
        return button.currentPrice <= GoldManager.GetInstance().Gold;
    }


    private void Start()
    {
        SetUpBuildingsButtons(InfosContainer.GetInstance().BuildingInfos);
    }

    public void SetUpBuildingsButtons(IEnumerable<BuildingInfo> buildingsToSet)
    {
        foreach (var mapObjectInfo in buildingsToSet)
        {
            var buildingShopButton = Instantiate(shopButtonPrefab, layoutGroupTransform, false);
            buildingShopButton.SetInfo(mapObjectInfo.title, mapObjectInfo.shopIcon, mapObjectInfo.enumType,
                mapObjectInfo.spawnPrice);
            buildingShopButton.ShopButtonClicked += OnShopButtonClicked;
            _buttons.Add(buildingShopButton);
            buildingShopButton.SetButtonEnable(GoldManager.GetInstance().Gold);
        }
    }

    private void OnDisable()
    {
        foreach (var buildingShopButton in _buttons)
        {
            buildingShopButton.ShopButtonClicked -= OnShopButtonClicked;
        }
    }
}