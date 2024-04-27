using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TDAs;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]

public class BuildingQueue 
{
    public QueueTDA<BuildingData> buildings = new QueueTDA<BuildingData>();
    public List<BuildingData> buildingList = new List<BuildingData>();
}
public class Province : MonoBehaviour
{
    [SerializeField] private ProvinceController provinceController;
    [SerializeField] private BuildingQueue buildingQueue;
    [SerializeField] private List<BuildingData> _buildings;

    private void OnMouseDown()
    {
        provinceController.SetSelectedProvince(this);
    }

    public void AddBuildingToQueue(BuildingData buildingData)
    {
        buildingQueue.buildings.Enqueue(buildingData);
        buildingQueue.buildingList.Add(buildingData);
        Debug.Log(buildingQueue.buildings.Count);
    }

    [ContextMenu("CreateBuilding")]
    public void CreateBuilding()
    {
        BuildingData lastBuildingData = buildingQueue.buildings.Peek();
        Debug.Log($"{lastBuildingData.name} creado");
        _buildings.Add(lastBuildingData);
        buildingQueue.buildings.Dequeue(lastBuildingData);
        buildingQueue.buildingList.RemoveAt(0);
    }
}
