using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using DefaultNamespace;
using Infos;
using UnityEngine;

namespace Spawners
{
    public class BuildingSpawner : AnyObjectSpawner<Building, BuildingType, BuildingInfo>
    {
        public List<Building> SpawnedBuildings { get; private set; }

        public event Action MapChanched;
        public IEnumerable<BuildingInfo> GetMapObjectsInfos() => ObjectDict.Values;

        public static BuildingSpawner GetInstance() => _instance;

        //TODO singltn instance для геев или нет?
        private static BuildingSpawner _instance;


        private void OnEnable()
        {
            _instance = this;
        }

        protected void Awake()
        {
            _instance = this;
            
            // TODO избавиться от синглтона а то что-то некрасиво
//            ObjectDict = InfosContainer.GetInstance().BuildingDict;
            SpawnedBuildings = new List<Building>();
        }

        private void Start()
        {
            ObjectDict = InfosContainer.GetInstance().BuildingDict;
        }


        public override T Spawn<T>(BuildingType type)
        {
            var mapObjectInfo = ObjectDict[type];
            var mapObject = Instantiate(mapObjectInfo.prefab.gameObject).GetComponent<T>();
            mapObjectInfo.SetParametersTo(mapObject);


            OnMapChanched();

            SpawnedBuildings.Add(mapObject);
            return mapObject;
        }

        IEnumerator WaitAndRun(float timeToWait, Action run)
        {
            yield return new WaitForSeconds(timeToWait);
            run();
        }


        public GameObject BluePrintSpawn(BuildingType type)
        {
            var bluePrint = Instantiate(ObjectDict[type].blueprintPrefab);

            return bluePrint;
        }

        //TODO  или UNITY ACTION
        public virtual void OnMapChanched()
        {
            StartCoroutine(WaitAndRun(0.1f, MapChangedInvoker));
        }

        private void MapChangedInvoker()
        {
            MapChanched?.Invoke();
        }
    }
}