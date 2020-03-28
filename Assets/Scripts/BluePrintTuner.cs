using System;
using System.Collections;
using DefaultNamespace;
using Spawners;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Buildings
{
    public class BluePrintTuner : MonoBehaviour
    {
        //TODO TEMP _instance
        public static BluePrintTuner GetInstance() => _instance;

        public event Action BuildingSpawned;
        public event Action SpawningCancel;

        private static BluePrintTuner _instance;

        private Transform _spawnedBluePrintTransform;
        private BuildingType _currentBuildingType;
        private Camera _camera;

        private void Awake()
        {
            _instance = this;
            _camera = Camera.main;
        }

        private void OnDisable()
        {
            PlayerInput.GetInstance().LeftMouseButtonClick -= SpawnCurrentBuilding;
        }

        private void SpawnCurrentBuilding(PlayerInput.MouseButtonClickArgs mouseButtonClickArgs)
        {
            if (!_spawnedBluePrintTransform) return;
            SpawnBuilding(_currentBuildingType, _spawnedBluePrintTransform);
//            PlayerInput.GetInstance().lastClickType = PlayerInput.LastClickTypes.SpawnBuildingClick;
        }


        public void SetBluePrint(BuildingType type)
        {
            if (_spawnedBluePrintTransform) Destroy(_spawnedBluePrintTransform.gameObject);

            _currentBuildingType = type;
            _spawnedBluePrintTransform = BuildingSpawner.GetInstance().BluePrintSpawn(type).transform;
            PlayerInput.GetInstance().LeftMouseButtonClick += SpawnCurrentBuilding;
            StartCoroutine(FitOnBluePrint(_spawnedBluePrintTransform, type));
        }


        private IEnumerator FitOnBluePrint(Transform bluePrintTransform, BuildingType buildingType)
        {
            RaycastHit hit;

            var heightOffset = bluePrintTransform.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2;

            while (bluePrintTransform)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    bluePrintTransform.position = hit.point + Vector3.up * heightOffset;
                }

                var mouseScrollDelta = Input.mouseScrollDelta;
                if (mouseScrollDelta.magnitude > 0)
                {
                    bluePrintTransform.Rotate(mouseScrollDelta * 10);
                }

                yield return null;
            }
        }


        public void SpawnBuilding(BuildingType buildingType, Transform bluePrintTransform)
        {
            var building = BuildingSpawner.GetInstance().Spawn<Building>(buildingType);
            var transform1 = building.transform;
            transform1.position = bluePrintTransform.position;
            transform1.rotation = bluePrintTransform.rotation;

            Destroy(bluePrintTransform.gameObject);
            _spawnedBluePrintTransform = null;
            PlayerInput.GetInstance().LeftMouseButtonClick -= SpawnCurrentBuilding;
            OnBuildingSpawned();
        }

        protected virtual void OnBuildingSpawned()
        {
            BuildingSpawned?.Invoke();
        }

        protected virtual void OnSpawningCancel()
        {
            SpawningCancel?.Invoke();
        }
    }
}