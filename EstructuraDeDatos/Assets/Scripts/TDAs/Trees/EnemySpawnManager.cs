using System;
using System.Collections;
using Assets.Scripts.Estructura_de_Datos;
using UnityEngine;

namespace TDAs.Trees
{
    public class EnemySpawnManager : MonoBehaviour
    {
        private AVLTree eventTree = new AVLTree();
        [SerializeField] private float timer = 20.0f; // Temporizador para la ejecución de eventos
        [SerializeField] private GameObject[] enemiesWaves;
        [SerializeField] private int enemiesWaveIndex;
        [SerializeField] private bool stillRunning;


        void Start()
        {
            stillRunning = true;
            AddEvent(20, SpawnEnemiesWave);
            AddEvent(40, SpawnEnemiesWave);
            AddEvent(60, SpawnEnemiesWave);
            AddEvent(80, SpawnEnemiesWave);
            StartCoroutine(EventExecutionRoutine());
        }

        public void AddEvent(float triggerTime, Action eventAction)
        {
            EventNode newEvent = new EventNode(triggerTime, eventAction);
            eventTree.Insert(newEvent);
        }

        private IEnumerator EventExecutionRoutine()
        {
            while (stillRunning)
            {
                yield return new WaitForSeconds(timer); // Esperar 20 segundos

                // Ejecutar el evento más próximo y removerlo del árbol
                if (eventTree.GetMin() != null)
                {
                    EventNode nextEvent = eventTree.GetMin().Data;
                    nextEvent.EventAction?.Invoke();
                    eventTree.Remove(nextEvent);
                }
            }

            Destroy(gameObject);
            Debug.Log("Se termino la ejecucion del arbol");
        }

        void SpawnEnemiesWave()
        {
            Instantiate(enemiesWaves[enemiesWaveIndex], Vector3.zero, Quaternion.identity);
            enemiesWaveIndex++;

            if (enemiesWaveIndex >= enemiesWaves.Length)
            {
                stillRunning = false;
            }

            Debug.Log("Se ejecuto el evento SpawnEnemies");
        }
    }
}