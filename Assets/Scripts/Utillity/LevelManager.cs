using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Analytics;

namespace Utillity {
    public class LevelManager : MonoBehaviour, ILevelManager {
        [SerializeField] private GameObject _spawnManager;

        [SerializeField] private List<GameObject> _levels;

        private GameObject _currentLevel;
        private SpawnManager _manager;

        private int _levelNumber;
        private bool _levelInProgress;
        private AudioSource _audioSource;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
            _currentLevel = _levels[0];
            _manager = _spawnManager.GetComponent<SpawnManager>();
        }

        private void Update() {
            if (!_levelInProgress) {
                if (_levelNumber < _levels.Count) {
                    _currentLevel = _levels[_levelNumber];
                    SpawnCurrentLevel();
                }
            }
        }

        private void SpawnCurrentLevel() {
            _levelInProgress = true;
            _levelNumber++;

            _manager.SpawnedObject = _currentLevel.GetComponent<Level>().SpawnedEnemy;
            _manager.NumberOfSpawnedObjects = _currentLevel.GetComponent<Level>().NumberOfEnemies;
            _manager.SpawnIntervalInSeconds = _currentLevel.GetComponent<Level>().SpawnIntervalInSeconds;
            _manager.SpreadDistance = _currentLevel.GetComponent<Level>().SpawnSpreadDistance;
            _manager.MovementSpeed = _currentLevel.GetComponent<Level>().MovementSpeed;

            Invoke("Spawn", _currentLevel.GetComponent<Level>().WaitTimeBeforSpawning);

            Invoke("SetLevelDone", _currentLevel.GetComponent<Level>().LevelTime);
        }

        private void SetLevelDone() {
            _levelInProgress = false;
        }

        private void Spawn() {
            var x = _currentLevel.GetComponent<Level>().SpawnSound;
            _audioSource.clip = x;
            _audioSource.Play();
            _manager.Spawn();
        }

        private void OnGUI() {
            GUI.Box(new Rect(10, 10, 150, 30), "Level number: " + (_levelNumber));
            GUI.Box(new Rect(10, 40, 150, 30), "Level in progress : " + _levelInProgress);
            GUI.Box(new Rect(10, 70, 150, 30), "Level Time:  " + _currentLevel.GetComponent<Level>().LevelTime);
            GUI.Box(new Rect(10, 100, 150, 30), "Levels remaining : " + (_levels.Count - _levelNumber));
        }
    }
}