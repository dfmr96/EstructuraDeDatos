using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Units/UnitData", fileName = "New UnitData")]
    public class UnitData : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite sprite;
        public Sprite bigSprite;
        public int atkDamage;
        public int defDamage;
        public int health;
        public float speed;
        public float range;
        public float atkRate;
        public float cost;
    }
}
