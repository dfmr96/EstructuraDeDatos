using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class Squad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private List<Units> units;
    [SerializeField] private TMP_Text unitsDisplay;
    [SerializeField] private Vector3 initPos = Vector3.zero;
    [SerializeField] private Vector3 endPos = Vector3.zero;
    [SerializeField] private Vector3 dir = Vector3.zero;
    [SerializeField] private int speed;

    private void Awake()
    {
        RefreshUnitsDisplay();
    }

    private void Update()
    {
        if (dir != Vector3.zero)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
        }
    }

    public void AddUnits(UnitData unitToAdd, int amount)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (unitToAdd == units[i].unitData)
            {
                Debug.Log($"Se han encontrado {units[i].amount}{units[i].unitData.name}");
                Units tempUnits = units[i];
                tempUnits.amount += amount;
                Debug.Log($"Se han agregado {amount}{unitToAdd.name}");
                units[i] = tempUnits;
                Debug.Log($"Ahora hay {units[i].amount}{units[i].unitData.name}");
                RefreshUnitsDisplay();
                return;
            }
        }
        
        units.Add(new Units(unitToAdd,amount));
        RefreshUnitsDisplay();
    }

    public void RefreshUnitsDisplay()
    {
        unitsDisplay.SetText("Units: \n");
        for (int i = 0; i < units.Count; i++)
        {
            unitsDisplay.SetText($"{unitsDisplay.text} \n{units[i].unitData.name}: {units[i].amount}");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Camera.main != null) initPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPos.z = 0;
        initPos.z = 0;
        //endPos = endPos.normalized; 
        dir = (endPos - initPos).normalized;
        
    }
}
[Serializable]
public struct Units
{
    public int amount;
    public UnitData unitData;

    public Units(UnitData unitData, int amount)
    {
        this.unitData = unitData;
        this.amount = amount;
    }

}
