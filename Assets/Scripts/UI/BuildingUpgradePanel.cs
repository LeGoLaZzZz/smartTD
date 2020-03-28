using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class BuildingUpgradePanel : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;

    [SerializeField] private Text sellCountText;
    [SerializeField] private Text upgradeCountText;
    [SerializeField] private Text buildingNameText;
    [SerializeField] private Text levelValueText;


    private Building _building;

    private Canvas _canvas;


    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _building = GetComponentInParent<Building>();
        if (Camera.main != null)
        {
            var rotation = Camera.main.transform.rotation;
            transform.rotation = new Quaternion(rotation.x, 0, 0, rotation.w);
        }
    }


    public void Upgrade()
    {
        BuildingUpgrader.UpgradeBuilding(_building);
        UpdateInfo();
    }


    public void Sell()
    {
        BuildingUpgrader.SellBuilding(_building);
        Destroy(this);
    }


    public void OpenPanel()
    {
        _canvas.enabled = true;

        UpdateInfo();
    }

    public void ClosePanel()
    {
        _canvas.enabled = false;
    }

    public void UpdateInfo()
    {
        sellCountText.text = _building.sellCost.ToString();
        upgradeCountText.text = _building.upgradeCost.ToString();
        levelValueText.text = _building.Level.ToString();
        buildingNameText.text = _building.Type.ToString();
    }
}