using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


[System.Serializable]

public class BuildingQueue
{
    public Queue<Building> buildings = new Queue<Building>();
    public List<Building> buildingList = new List<Building>();
}
public class Province : MonoBehaviour
{
    [SerializeField] private BuildingController buildingController;
    [SerializeField] private BuildingQueue buildingQueue;
    [SerializeField] private List<Building> _buildings;

    private void OnMouseDown()
    {
        buildingController.SetSelectedProvince(this);
    }

    public void AddBuildingToQueue(Building building)
    {
        buildingQueue.buildings.Enqueue(building);
        buildingQueue.buildingList.Add(building);
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
