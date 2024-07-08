using System.Collections;
using System.Collections.Generic;
using TDAs.QuickSort;
using UnityEngine;

public class UnitScrollList : MonoBehaviour
{
    public List<UnitScroll> unitList;
    public QuickSortUnits quickSortUnits;

    [ContextMenu("ReOrderList")]
    public void UpdateList()
    {
        unitList.Clear();
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            UnitScroll tempUnitScroll = transform.GetChild(i).GetComponent<UnitScroll>();
            unitList.Add(tempUnitScroll);
        }

        unitList = quickSortUnits.QuickSort(unitList);
        unitList.Reverse();

        for (int i = 0; i < unitList.Count; i++)
        {
            unitList[i].transform.SetSiblingIndex(i);
        }
    }
}
