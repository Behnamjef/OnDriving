using System;
using MagicOwl;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LevelCompletePanel : CommonUIBehaviour
    {
        private int _levelCoin;

        [SerializeField] private Text coinCount;
        [SerializeField] private Text normalCoinCount;
        [SerializeField] private Text multipleCoinCount;

        [SerializeField] private Transform meterNeedle;
        [SerializeField] private float needleSpeed = 1;

        private float needleSpeedPace;
        private bool needleSpeedDirection = true;

        private float needleRightAngle = 60;
        private float needleLeftAngle = -70;

        private bool _stopNeedle;
        private void Start()
        {
            _levelCoin = 100;
            coinCount.text = "+" + _levelCoin;
            normalCoinCount.text = "+" + _levelCoin;
            RebuildAllRects();
        }

        private void Update()
        {
            if(_stopNeedle) return;
            needleSpeedPace += needleSpeed * Time.deltaTime * (needleSpeedDirection ? 1 : -1);
            if (needleSpeedPace <= 1 && needleSpeedPace >= 0)
            {
                meterNeedle.eulerAngles = Vector3.forward * Mathf.Lerp(60, -70, needleSpeedPace);
                multipleCoinCount.text = "+" + (int)Mathf.Lerp(_levelCoin * 2, _levelCoin * 6, needleSpeedPace);
            }
            else
            {
                needleSpeedPace = needleSpeedDirection ? 1 : 0;
                needleSpeedDirection = !needleSpeedDirection;
            }
        }

        public void WatchVideoForMultipleCoin()
        {
            _stopNeedle = true;
            AdManager.Instance.OnAdClosed = OnAdClosed;
            AdManager.Instance.OnRewardAdComplete = RewardAdComplete;
            AdManager.Instance.ShowRewardAd();
        }

        private void RewardAdComplete()
        {
            var levelCoin = (int)Mathf.Lerp(_levelCoin * 2, _levelCoin * 6, needleSpeedPace);
            var coins = PlayerPrefs.GetInt("LevelCoin", 0);
            PlayerPrefs.SetInt("LevelCoin", coins + levelCoin);
            InputManager.Instace.UpdateCoinsText();
        }

        public void SkipSpeedMeter()
        {
            var coins = PlayerPrefs.GetInt("LevelCoin", 0);
            PlayerPrefs.SetInt("LevelCoin", coins + _levelCoin);
            AdManager.Instance.OnAdClosed = OnAdClosed;
            AdManager.Instance.ShowInterstitial();
            InputManager.Instace.UpdateCoinsText();
        }

        private void OnAdClosed()
        {
            SetActive(false);
            Close();
        }
        
        private void Close()
        {
            InputManager.Instace.ShowUnlockPanel();
        }
    }
}