using System;
using System.Collections.Generic;
using TDAs;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerCastle : Player
    {
        public float currentHealth;
        private float damageMultiplier = 1;
        
        [SerializeField] private BuildingQueue buildingQueue;
        [field: SerializeField] public List<Building> Buildings { get; private set; }

        [SerializeField] private float timeToBuild;
        [SerializeField] private float buildTimer;

        [SerializeField] private Building wallBuilding;
        [SerializeField] private Building barrackBuilding;
        [SerializeField] private Building workshopBuilding;
        
        [SerializeField] private Button tankBtn;
        [SerializeField] private Button artillerybtn;
        [SerializeField] private Button workshopbtn;
        [SerializeField] private Button barrackBtn;
        [SerializeField] private Button wallBtn;
        private bool underConstruction;
        
        public NestedStack<string> constructionLog = new NestedStack<string>();

        private float currentTime = 0;
        private void Start()
        {
            currentHealth = health;
            buildingQueue.buildings.Initialize(10);
            barrackBuilding = Buildings[0];
            workshopBuilding = Buildings[1];
            wallBuilding = Buildings[2];

        }

        public override void TakeDamage(float damageTaken)
        {

            currentHealth -= damageTaken * damageMultiplier;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (underConstruction)
            {
                buildTimer += Time.deltaTime;

                if (buildTimer >= timeToBuild)
                {
                    CreateBuilding();
                    buildTimer = 0;
                }
            }
        }

        public void AddBuildingToQueue(BuildingData buildingData)
        {
            buildingQueue.buildings.Enqueue(buildingData);
            buildingQueue.buildingList.Add(buildingData);
            Debug.Log(buildingQueue.buildings.Count);
            
            if (!underConstruction) BeginConstruction();
        }

        public void BeginConstruction()
        {
            if (underConstruction) return;

            timeToBuild = buildingQueue.buildings.Peek().upgradeTime;
            buildTimer = 0;
            underConstruction = true;
        }
        
        public void CreateBuilding()
        {
            buildTimer = 0;
            underConstruction = false;
            BuildingData buildToCreate = buildingQueue.buildings.Peek();


            Building selectedBuilding = null;
            foreach (Building building in Buildings)
            {
                if (building._buildingData == buildToCreate)
                {
                    selectedBuilding = building;
                    building.buildingLevel++;
                    break;
                }
            }

            if (selectedBuilding != null)
            {
                string constructionTime = $"{selectedBuilding._buildingData.name} a nivel {selectedBuilding.buildingLevel} en {currentTime.ToString("00.00")}s";
                Debug.Log($"{constructionTime}");
                constructionLog.Push(constructionTime);
            }

            CheckBuilding();
            buildingQueue.buildingList.RemoveAt(0);
            buildingQueue.buildings.Dequeue();
            
            if (!buildingQueue.buildings.IsEmpty()) BeginConstruction();
        }

        public void CheckBuilding()
        {
            if (barrackBuilding.buildingLevel == 2)
            {
                workshopbtn.interactable = true;
                barrackBtn.interactable = false;
            }

            if (workshopBuilding.buildingLevel == 1)
            {
                tankBtn.interactable = true;
            }

            if (workshopBuilding.buildingLevel == 2)
            {
                artillerybtn.interactable = true;
                workshopbtn.interactable = false;
            }

            if (wallBuilding.buildingLevel == 1)
            {
                damageMultiplier = 0.8f;
            }

            if (wallBuilding.buildingLevel == 2)
            {
                damageMultiplier = 0.6f;
                wallBtn.interactable = false;
            }
        }
    }

    [Serializable]
    public class Building
    {
        public BuildingData _buildingData;
        public float buildingLevel;
    }
}