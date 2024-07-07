using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingButton : MonoBehaviour
{

    [SerializeField] private BuildingData _buildingData;
    [SerializeField] private PlayerCastle _playerCastle;


    public void UpgradeBuilding()
    {
        //unitData = unitPrefab.GetComponent<Unit>().UnitData;
        if (GameManager.Instance.coins > _buildingData.cost)
        {
            GameManager.Instance.coins -= _buildingData.cost;
           _playerCastle.AddBuildingToQueue(_buildingData);
        }
    }
}
