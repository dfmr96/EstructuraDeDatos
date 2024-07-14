using System.Collections;
using System.Collections.Generic;
using TDAs;
using TDAs.Graphs;
using UnityEngine;

public class EnemyWave: MonoBehaviour
{
    [SerializeField] private EnemyUnit[] enemies;
    private List<GraphNode<City>>[] unitsPath;

    public void MoveAllUnits()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].MoveUnitAlongPath(unitsPath[i]);
        }
    }
    public void MoveAllUnits(int delay)
    {
        StartCoroutine(MoveUnitsWithOffset(delay));
    }

    public IEnumerator MoveUnitsWithOffset(int delay)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].MoveUnitAlongPath(unitsPath[i]);
            yield return new WaitForSeconds(delay);
        }

    }

    public void SetPaths(List<GraphNode<City>>[] unitsPath)
    {
        this.unitsPath = unitsPath;
    }

    public void SetInitPosition(Vector3 position)
    {
        foreach (var enemy in enemies)
        {
            enemy.transform.position = position;
        }
    }
}