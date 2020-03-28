using System;
using System.Linq;
using Infos;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Spawners
{
    public class UnitSpawner : AnyObjectSpawner<Unit, UnitType, UnitInfo>
    {
        //TODO instance
        private static UnitSpawner _instance;
        public static UnitSpawner GetInstance() => _instance;


        protected void Awake()
        {
            _instance = this;

            // TODO избавиться от синглтона а то что-то некрасиво
//            ObjectDict = InfosContainer.GetInstance().UnitDict;
        }

        private void Start()
        {
            ObjectDict = InfosContainer.GetInstance().UnitDict;
        }

        public override T Spawn<T>(UnitType type)
        {
            var mapUnitInfo = ObjectDict[type];
            var mapUnit = Instantiate(mapUnitInfo.prefab.gameObject).GetComponent<T>();

            mapUnitInfo.SetParametersTo(mapUnit);
            mapUnit.GetComponent<NavMeshAgent>().Warp(transform.position);


            return mapUnit;
        }


        public void Spawn()
        {
            var mapUnitInfo = ObjectDict.First();
            var mapUnit = Instantiate(mapUnitInfo.Value.prefab.gameObject).GetComponent<Unit>();

            mapUnitInfo.Value.SetParametersTo(mapUnit);
            mapUnit.GetComponent<NavMeshAgent>().Warp(transform.position);
        }
    }
}