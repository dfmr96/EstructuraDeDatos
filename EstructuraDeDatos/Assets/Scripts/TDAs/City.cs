using DefaultNamespace;
using TDAs.Graphs;
using UnityEngine;

namespace TDAs
{
    public class City: MonoBehaviour
    {
        public string cityName;
        public TextMesh nameTextMesh;

        public void SetCityName(string newName)
        {
            cityName = newName;
            nameTextMesh.text = newName;
        }
        
        private void Start()
        {
            var clickable = GetComponent<ClickableObject>();
            if (clickable != null)
            {
                clickable.OnLeftClick.AddListener(OnCityClicked);
            }
        }
        
        private void OnCityClicked()
        {
            if (UnitManager.instance.selectedUnit != null)
            {
                CityManager.instance.MoveUnitBetweenCities(UnitManager.instance.selectedUnit, this);
            }
            else
            {
                Debug.Log("No hay unidad seleccionada para mover.");
            }
        }
    }
}