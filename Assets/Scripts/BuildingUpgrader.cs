using System;
using Buildings;
using Infos;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class BuildingUpgrader : MonoBehaviour
    {
        private Camera _camera;
        private BuildingUpgradePanel _openedPanel;

        public static void UpgradeBuilding(Building building)
        {
            if (GoldManager.GetInstance().TryTakeGold(building.upgradeCost))
            {
                var buildingInfo = InfosContainer.GetInstance().BuildingDict[building.Type];
                buildingInfo.SetParametersTo(building, building.Level + 1);
            }
        }

        public static void SellBuilding(Building building)
        {
            GoldManager.GetInstance().AddGold(building.sellCost);
            Destroy(building.gameObject);
        }


        private void Awake()
        {
            _camera = Camera.main;
        }


        private void Start()
        {
            PlayerInput.GetInstance().LeftMouseButtonClick += ClickOnMap;
        }


        private void OnDisable()
        {
            PlayerInput.GetInstance().LeftMouseButtonClick -= ClickOnMap;
        }

        private void OnDestroy()
        {
            PlayerInput.GetInstance().LeftMouseButtonClick -= ClickOnMap;
        }


        public void ClickOnMap(PlayerInput.MouseButtonClickArgs mouseButtonClickArgs)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;


            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(mouseButtonClickArgs.MousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var buildingUpgradePanel = hit.collider.GetComponentInChildren<BuildingUpgradePanel>();

                if (buildingUpgradePanel && _openedPanel != buildingUpgradePanel)
                {
                    if (_openedPanel != null) _openedPanel.ClosePanel();
                    _openedPanel = null;

                    buildingUpgradePanel.OpenPanel();
                    _openedPanel = buildingUpgradePanel;
                }
                else
                {
                    if (_openedPanel != null) _openedPanel.ClosePanel();
                    _openedPanel = null;
                }
            }
            else
            {
                if (_openedPanel != null) _openedPanel.ClosePanel();
                _openedPanel = null;
            }
        }
    }
}