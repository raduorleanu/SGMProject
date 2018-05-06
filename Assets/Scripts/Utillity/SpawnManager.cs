using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Utillity {
    public class SpawnManager : MonoBehaviour {
        
        [HideInInspector]
        public int NumberOfSpawnedObjects;
        
        [HideInInspector]
        public float SpawnIntervalInSeconds;
        
        [HideInInspector]
        public float SpreadDistance;

        [HideInInspector]
        public GameObject SpawnedObject;
        
        [HideInInspector]
        public float MovementSpeed;

        public void Spawn() {
            InvokeRepeating("Spawn_", 0, SpawnIntervalInSeconds);
        }

        private Vector3 CalculateSpawnPoint() {
            
            // get some random x and z points, they is inherited from the parent
            
            var parentPosition = GameObject.Find("SpawnArea1").transform.position;
            var randomX = Random.Range(parentPosition.x - SpreadDistance, parentPosition.x + SpreadDistance);
            var randomZ = Random.Range(parentPosition.z - SpreadDistance, parentPosition.z + SpreadDistance);

            return new Vector3(randomX, parentPosition.y, randomZ);
        }

        private void Spawn_() {
            NumberOfSpawnedObjects--;
            var obj = Instantiate(SpawnedObject);
            obj.gameObject.transform.position = CalculateSpawnPoint();
            obj.gameObject.GetComponent<NavMeshAgent>().speed = MovementSpeed;
            if (NumberOfSpawnedObjects == 0) {
                CancelInvoke("Spawn_");
            }
        }
        
        public void SpawnSingleObject(GameObject spawnedObject) {
            var obj = Instantiate(spawnedObject);
            obj.gameObject.transform.position = CalculateSpawnPoint();
            //obj.gameObject.GetComponent<NavMeshAgent>().speed = MovementSpeed;
        }

    }
}