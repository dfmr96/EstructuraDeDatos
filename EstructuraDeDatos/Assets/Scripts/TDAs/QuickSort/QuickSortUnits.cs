using System.Collections.Generic;
using Assets.Scripts.Estructura_de_Datos.Interfaces;
using UnityEngine;

namespace TDAs.QuickSort
{
    public class QuickSortUnits : MonoBehaviour , IQuickSort
    {
        // Método público que inicia el QuickSort
        public List<UnitScroll> QuickSort(List<UnitScroll> list)
        {
            QuickSortInternal(list, 0, list.Count - 1);
            return list;
        }

        // Método interno de QuickSort para realizar la ordenación usando índices
        private void QuickSortInternal(List<UnitScroll> list, int low, int high)
        {
            if (low < high)
            {
                int pivot = Partition(list, low, high);

                QuickSortInternal(list, low, pivot - 1);
                QuickSortInternal(list, pivot + 1, high);
            }
        }

        // Método para realizar la partición de la lista
        private int Partition(List<UnitScroll> list, int low, int high)
        {
            UnitScroll pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                // Cambio aquí para ordenar de mayor a menor
                if (list[j].currentHealth >= pivot.currentHealth)
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, high);
            return i + 1;
        }

        // Método de utilidad para intercambiar elementos en la lista
        private void Swap(List<UnitScroll> list, int i, int j)
        {
            UnitScroll temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        // Método para imprimir la lista (útil para debugging)
        private void PrintList(List<Score> list)
        {
            foreach (Score score in list)
            {
                Debug.Log("Player: " + score.playerName + ", Score: " + score.score);
            }
        }
    }
}