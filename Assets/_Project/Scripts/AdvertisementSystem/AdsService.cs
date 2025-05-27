using System;
using AdvertisementSystem.Static_Data;
using Cysharp.Threading.Tasks;
using Common.Coroutines;
using Gameplay.Scoring;
using System.Threading;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace AdvertisementSystem
{
    public class AdsService : IInitializable, IDisposable, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private readonly AdsSettings _settings;
        private readonly ICoroutineRunService _coroutineRunner;
        private readonly IScoreService _scoreService;
        private string _gameId;
        private string _bannerAdUnitId;
        private string _interstitialAdUnitId;
        private bool _isBannerLoaded;
        private bool _isInterstitialLoaded;
        private CancellationTokenSource _cts;

        public AdsService(AdsSettings settings, ICoroutineRunService coroutineRunner, IScoreService scoreService)
        {
            _settings = settings;
            _coroutineRunner = coroutineRunner;
            _scoreService = scoreService;
            _cts = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _gameId = _settings.GameId;
            _bannerAdUnitId = _settings.BannerAdUnitId;
            _interstitialAdUnitId = _settings.InterstitialAdUnitId;

            if (string.IsNullOrEmpty(_gameId))
                return;

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _settings.TestMode, this);
                _coroutineRunner.StartCoroutine(LoadBannerAsync().ToCoroutine());
                _coroutineRunner.StartCoroutine(LoadInterstitialAsync().ToCoroutine());
            }
        }

        public async UniTask ShowInterstitialAsync()
        {
            if (!_isInterstitialLoaded)
            {
                await LoadInterstitialAsync();
                return;
            }

            Time.timeScale = 0f;
            Advertisement.Show(_interstitialAdUnitId, this);
        }

        public void ShowBanner()
        {
            if (!_isBannerLoaded)
                return;

            BannerOptions options = new BannerOptions { };

            Advertisement.Banner.Show(_bannerAdUnitId, options);
        }

        public void HideBanner()
        {
            Advertisement.Banner.Hide();
            _isBannerLoaded = false;
        }

        private async UniTask LoadBannerAsync()
        {
            if (!Advertisement.isInitialized)
                await WaitForAdsInitializationAsync();

            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = () =>
                {
                    _isBannerLoaded = true;
                    ShowBanner();
                },
                errorCallback = message => _isBannerLoaded = false
            };

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Load(_bannerAdUnitId, options);
        }

        private async UniTask LoadInterstitialAsync()
        {
            if (!Advertisement.isInitialized)
                await WaitForAdsInitializationAsync();

            Advertisement.Load(_interstitialAdUnitId, this);
        }

        private async UniTask WaitForAdsInitializationAsync()
        {
            while (!Advertisement.isInitialized && !_cts.Token.IsCancellationRequested)
                await UniTask.Yield();
        }

        public void OnInitializationComplete()
        {

        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {

        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            if (adUnitId == _interstitialAdUnitId)
                _isInterstitialLoaded = true;
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            if (adUnitId == _interstitialAdUnitId)
                _isInterstitialLoaded = false;
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            Time.timeScale = 1f;
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                _scoreService.AddScore(_settings.InterstitialRewardScore);

            _isInterstitialLoaded = false;
            _coroutineRunner.StartCoroutine(LoadInterstitialAsync().ToCoroutine());
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Time.timeScale = 1f;
            _isInterstitialLoaded = false;
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            HideBanner();
        }
    }
}