using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utillity {
    public class ProximityProvider : MonoBehaviour {
        private static ProximityProvider _proximityProvider;

        public static ProximityProvider Instance {
            get {
                if (_proximityProvider == null) {
                    _proximityProvider = FindObjectOfType(typeof(ProximityProvider)) as ProximityProvider;
                }

                return _proximityProvider;
            }
        }

        public static GameObject FindEnemyInRange(GameObject player, float range) {
            if (GameObject.FindWithTag("Enemy") == null) {
                // return itself if no enemy present, maybe change this to some default position or object
                return null;
            }
            return GameObject
                .FindGameObjectsWithTag("Enemy")
                .FirstOrDefault(i => Vector3.Distance(player.transform.position, i.transform.position) <= range);
        }

        public static GameObject FindClosestEnemyAsPlayer(GameObject player) {
            if (GameObject.FindWithTag("Enemy") == null) {
                Debug.Log("No enemy");
                // return itself if no enemy present, maybe change this to some default position or object
                return player;
            }

            return GameObject.FindGameObjectsWithTag("Enemy")
                .OrderBy(i => Vector3.Distance(player.transform.position, i.gameObject.transform.position))
                .FirstOrDefault();
        }

        //		public static IEnumerable<GameObject> FindClosestEnemyInRange(GameObject player, float radius)
//		{
//			var enemy = FindClosestEnemyAsPlayer(player);
//			var x = GameObject.FindGameObjectsWithTag("Enemy")
//				.Where(i => Vector3.Distance(i.transform.position, enemy.transform.position) < radius);
//			return x;
//		}
//		
        public static IEnumerable<GameObject> FindEnemiesAround(GameObject referenceObject, float radius) {
            var x = GameObject.FindGameObjectsWithTag("Enemy")
                .Where(i => Vector3.Distance(i.transform.position, referenceObject.transform.position) < radius);
            return x;
        }

        public static GameObject[] GetPlayersPosition() {
            return GameObject.FindGameObjectsWithTag("Player");
        } 
    }
}