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
    }
}