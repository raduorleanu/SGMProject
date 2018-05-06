using UnityEngine;
using Utillity;

namespace DefaultNamespace
{
    public class EnemySpell: MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer != 11){
                Destroy(this.gameObject);
            }
            
            hurtPlayer(other.gameObject);
            
        }

        void hurtPlayer(GameObject player)
        {
            
            player.GetComponent<Player>().SubstarctDamageFromLife_(10);
            player.GetComponent<HealthBar>().ShrinkBar(10, player.GetComponent<Player>().OriginalHealth);
        }
    }
}