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
            if (CityManager.instance.selectedUnit != null)
            {
                CityManager.instance.MoveUnitBetweenCities(CityManager.instance.selectedUnit, this);
            }
            else
            {
                Debug.Log("No hay unidad seleccionada para mover.");
            }
        }

        private void OnClickCity()
        {
            if (CityManager.instance.selectedUnit != null)
            {
                CityManager.instance.MoveUnitBetweenCities(CityManager.instance.selectedUnit, this);
            }
            else
            {
                Debug.Log("No hay unidad seleccionada para mover.");
            }
        }
    }
}