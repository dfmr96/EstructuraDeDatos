using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]

public class BuildingQueue
{
    public Queue<BuildingData> buildings = new Queue<BuildingData>();
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
        Debug.Log($"{buildingQueue.buildings.Peek().name} creado");
        _buildings.Add(buildingQueue.buildings.Peek());
        buildingQueue.buildings.Dequeue();
        buildingQueue.buildingList.RemoveAt(0);
    }
}
