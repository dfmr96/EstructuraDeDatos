using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class AddBuilding : MonoBehaviour
{
    [SerializeField] private ProvinceController provinceController;
    [SerializeField] private BuildingData buildingDataToCreate;

    public void AddBuildingToProvince()
    {
        Province pronvince = provinceController.GetSelectedProvince();
        
        pronvince.AddBuildingToQueue(buildingDataToCreate);
    }
}
