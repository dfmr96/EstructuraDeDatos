using Data;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

public class CreateUnitButton : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private UnitData unitData;

    public GameObject unitListContent;

    public UnitScrollList unitScrollList;
    
    public void Create()
    {
        //unitData = unitPrefab.GetComponent<Unit>().UnitData;
        Debug.Log($"{unitData.cost}");
        if (GameManager.Instance.coins > unitData.cost)
        {
            GameManager.Instance.coins -= unitData.cost;
            GameObject newUnit = Instantiate(unitPrefab, spawnPoint.position, quaternion.identity);
            newUnit.GetComponent<MovableUnit>().InitUnit(unitData, unitListContent, unitScrollList);
            
        }
    }
}
