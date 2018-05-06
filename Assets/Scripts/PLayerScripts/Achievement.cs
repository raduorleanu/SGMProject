using System.Runtime.InteropServices;
using Interfaces;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace PLayerScripts
{
    public class Achievement : MonoBehaviour
    {
        [SerializeField] private int _reqForAchievement;

        [SerializeField] private GameObject _achievmentImage;

        [SerializeField] private GameObject _achievementGuiOverlay;

//        [SerializeField] private GameObject _achievementNotification;

        public int KillCounter = 0;
//        private float _notificationDuration;

        // event
        public delegate void GeneralKillCounter(int reqForAchievement, GameObject player);

        public static event GeneralKillCounter EnemyKillCounterEvent;

        private void Start()
        {
//            _notificationDuration = 10f;
            Enemy.IncrementCounter += IncrementCounter;
            _achievementGuiOverlay.transform.localScale = _achievmentImage.transform.localScale;
            _achievementGuiOverlay.transform.position = _achievmentImage.transform.position;
        }

//        private void AddAchievement()
//        {
//        _achievementGuiOverlay.SetActive(false);
//            ShowNotificationOnUI();
//        }

        private void Update()
        {
        }

//        void ShowNotificationOnUI()
//        {
//            _achievementNotification.SetActive(true);
//            _notificationDuration -= 1;
//            if (_notificationDuration <= 0)
//            {
//                _achievementNotification.SetActive(false);
//                CancelInvoke("ShowNotificationOnUI");
//            }
//        }

        void IncrementCounter(int playerCode)
        {
            if (gameObject.GetHashCode() != playerCode) return;
            KillCounter++;
            if (EnemyKillCounterEvent != null) EnemyKillCounterEvent(_reqForAchievement, gameObject);
            if (KillCounter >= _reqForAchievement)
            {
                _achievementGuiOverlay.SetActive(false);
//                Debug.Log("Congratulations! You have unlocked an achievement!");
            }
        }

        private void OnDisable()
        {
            Enemy.IncrementCounter -= IncrementCounter;
        }
    }
}