using UnityEngine;

namespace Utillity {
    public class HealingZone : MonoBehaviour {
        private void OnTriggerStay(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                other.gameObject.GetComponent<Player>().Heal(1);
            }
        }
    }
}