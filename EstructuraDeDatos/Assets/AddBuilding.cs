using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AddBuilding : MonoBehaviour
{
    [SerializeField] private BuildingController _buildingController;
    [SerializeField] private Building _buildingToCreate;

    public void AddBuildingToProvince()
    {
        Province pronvince = _buildingController.GetSelectedProvince();
        
        pronvince.AddBuildingToQueue(_buildingToCreate);
    }
}
