using UnityEngine;

namespace Utillity {
    public class Level : MonoBehaviour {
        public GameObject SpawnedEnemy;

        public int NumberOfEnemies;

        public int LevelTime;

        public int WaitTimeBeforSpawning;

        public float SpawnIntervalInSeconds;

        public float SpawnSpreadDistance;

        public float MovementSpeed;

        public AudioClip SpawnSound;
    }
}