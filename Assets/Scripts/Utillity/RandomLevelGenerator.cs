using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utillity {
    public class RandomLevelGenerator : MonoBehaviour, ILevelManager {
        [SerializeField] private GameObject _spawnManager;

        public int NumberOfEnemiesPerWave = 2;
        public int TimeBetweenWaves = 10;

        private GameObject[] _enemies;
        private int _level = 1;
        private int _count;

        private SpawnManager _manager;

//        private string[] _enemyNames = new string[] {
//            "banana", "butterfly", "mushroom", "crab", "spider",
//            "mushroom", "butterfly", "banana"
//        };

        private readonly string[] _enemyNames = new string[] {
            "troll", "golem", "dragon", "spider", "crab",
            "mushroom", "butterfly", "banana"
        };

        private readonly int[] _chances = new int[] {2, 10, 150, 200, 250, 300, 470, 980};

        private int[] _results;

        private void Awake() {
            var x = Resources.LoadAll("Prefabs/Enemies");
            _results = new int[_chances.Length];
            _enemies = new GameObject[x.Length];
            for (var i = 0; i < x.Length; i++) {
                _enemies[i] = (GameObject) x[i];
            }

            _manager = _spawnManager.GetComponent<SpawnManager>();
            InvokeRepeating("StartSpawning", 3, TimeBetweenWaves);

//            _chances.Add("banana", 80);
//            _chances.Add("butterfly", 70);
//            _chances.Add("mushroom", 60);
//            _chances.Add("crab", 50);
//            _chances.Add("spider", 40);
//            _chances.Add("dragon", 30);
//            _chances.Add("golem", 20);
//            _chances.Add("troll", 10);
        }

        private GameObject GetAnEnemyBasedOnChance() {
            var chance = Random.Range(1, _chances.Sum());
//            Debug.Log("rnd chance: " + chance + ", sum: " + _chances.Sum());
            var cumulativeSum = new int[_chances.Length];
            for (var i = 0; i < cumulativeSum.Length; i++) {
                cumulativeSum[i] = _chances[i] + (i == 0 ? 0 : cumulativeSum[i - 1]);
            }

            var chosen = "";

            for (var i = 0; i < cumulativeSum.Length; i++) {
                if (chance > cumulativeSum[i]) continue;
                chosen = _enemyNames[i];
                break;
            }

            //Debug.Log(chosen);

            return _enemies.FirstOrDefault(enemy => enemy.name.ToLower().Contains(chosen));
        }

        private void StartSpawning() {
//            for (var i = 0; i < NumberOfEnemiesPerWave; i++) {
//                var x = GetAnEnemyBasedOnChance();
//                _manager.SpawnSingleObject(x);
//            }
            InvokeRepeating("SpawnSingleEnemy", 0, 0.3f);
            
        }

        private void SpawnSingleEnemy() {
            _count++;
            if (_count >= NumberOfEnemiesPerWave) {
                NumberOfEnemiesPerWave += 1;
                MakeNextWaveBetter();
                CancelInvoke("SpawnSingleEnemy");
                _count = 0;
            }

            var x = GetAnEnemyBasedOnChance();
            _manager.SpawnSingleObject(x);
        }

        private void MakeNextWaveBetter() {
            for (var i = 0; i < _chances.Length; i++) {
                if (i < _chances.Length / 2) {
                    _chances[i] += NumberOfEnemiesPerWave;
                }
            }
        }

        private IEnumerable<GameObject> LevelGenerator() {
            var l = new List<GameObject>();

            foreach (var enemy in _enemies) {
                var rnd = Random.Range(_level, _level * 2);
                for (var i = 0; i < rnd; i++) {
                    l.Add(enemy);
                }
            }

            _level++;
            return l;
        }
    }
}