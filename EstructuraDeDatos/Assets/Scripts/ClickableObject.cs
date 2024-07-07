using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class ClickableObject : MonoBehaviour
    {
        public UnityEvent OnLeftClick; // Evento para clic izquierdo
        public UnityEvent OnRightClick; // Evento para clic derecho

        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnLeftClick.Invoke();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                OnRightClick.Invoke();
            }
        }
    }
}