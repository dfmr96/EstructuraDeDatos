using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    public MovableUnit selectedUnit;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SelectUnit(MovableUnit unit)
    {
        if (selectedUnit != null)
        {
            selectedUnit.Deselect();  // Deselecciona la unidad actualmente seleccionada
        }

        selectedUnit = unit;
        if (selectedUnit != null)
        {
            selectedUnit.Select();  // Selecciona la nueva unidad
        }
    }
}
