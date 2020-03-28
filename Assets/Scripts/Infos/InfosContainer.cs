using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buildings;
using Units;
using UnityEngine;

namespace Infos
{
    public class InfosContainer : MonoBehaviour
    {
        public Dictionary<BuildingType, BuildingInfo> BuildingDict
        {
            get => _buildingDict;
            set => _buildingDict = value;
        }

        public Dictionary<UnitType, UnitInfo> UnitDict
        {
            get => _unitDict;
            set => _unitDict = value;
        }


        public BuildingInfo[] BuildingInfos
        {
            get => buildingInfos;
            set => buildingInfos = value;
        }

        public UnitInfo[] UnitInfos
        {
            get => unitInfos;
            set => unitInfos = value;
        }


        [SerializeField] private BuildingInfo[] buildingInfos;
        [SerializeField] private UnitInfo[] unitInfos;


        private Dictionary<BuildingType, BuildingInfo> _buildingDict = new Dictionary<BuildingType, BuildingInfo>();
        private Dictionary<UnitType, UnitInfo> _unitDict = new Dictionary<UnitType, UnitInfo>();


        private static InfosContainer _instance;
        public static InfosContainer GetInstance() => _instance;

        private void Awake()
        {
            _instance = this;
            BuildingInfos = buildingInfos;
            UnitInfos = unitInfos;


            BuildingInfos.All(info =>
            {
                BuildingDict.Add(info.enumType, info);
                return true;
            });

            UnitInfos.All(info =>
            {
                UnitDict.Add(info.enumType, info);
                return true;
            });
        }
    }
}