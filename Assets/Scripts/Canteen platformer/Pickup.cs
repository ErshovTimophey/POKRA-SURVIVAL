using UnityEngine;
using Visual;

namespace Core
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private PickupVisual pickupHandler;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                pickupHandler.UpdateAmount();
                Destroy(gameObject);
            }
        }
    }
}
