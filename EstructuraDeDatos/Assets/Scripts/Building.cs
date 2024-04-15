using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Buildings/BuildingData", fileName = "New BuildingData")]
    public class Building : ScriptableObject
    {
        public string name;
        public int level;
        public int maxLevel;
        public int upgradeTime;
    }
}