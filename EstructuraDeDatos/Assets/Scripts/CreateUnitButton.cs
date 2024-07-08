using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class CreateUnitButton : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private UnitData unitData;
    
    public void Create()
    {
        //unitData = unitPrefab.GetComponent<Unit>().UnitData;
        Debug.Log($"{unitData.cost}");
        if (GameManager.Instance.coins > unitData.cost)
        {
            GameManager.Instance.coins -= unitData.cost;
            Instantiate(unitPrefab, spawnPoint);
        }
    }
}
