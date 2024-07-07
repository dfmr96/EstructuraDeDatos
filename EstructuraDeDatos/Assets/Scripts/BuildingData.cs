using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Buildings/BuildingData", fileName = "New BuildingData")]
    public class BuildingData : ScriptableObject
    {
        public new string name;
        public int level;
        public int maxLevel;
        public int upgradeTime;
        public float cost;
    }
}