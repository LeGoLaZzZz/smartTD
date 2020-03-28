using System;
using Buildings;
using DefaultNamespace;
using Spawners;
using Units;
using UnityEngine;


public class InputWhat : MonoBehaviour
{
    [SerializeField] private UnitSpawner spawner;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }


    //TODO TEMP CODE
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            BluePrintTuner.GetInstance().SetBluePrint(BuildingType.SimpleWall);
        }
        if (Input.GetMouseButtonDown(0))
        {
           
        }
        
        
        
    }
}