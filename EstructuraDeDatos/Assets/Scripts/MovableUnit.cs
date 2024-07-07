using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovableUnit : MonoBehaviour
    {
        public float speed = 1f; // Speed of the unit in units per second

        public void MoveToCity(Vector3 destination)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }
            StartCoroutine(MoveToDestination(destination));
        }

        private IEnumerator MoveToDestination(Vector3 destination)
        {
            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                yield return null;
            }

            // Optional: Call a method or send a message when the destination is reached
            OnDestinationReached();
        }

        private void OnDestinationReached()
        {
            // Logic to handle what happens when the unit reaches its destination
            Debug.Log("Destination reached");
        }
    }
}