using AdvertisementSystem.Static_Data;
using Cysharp.Threading.Tasks;
using Common.Coroutines;
using Gameplay.Scoring;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;
using System.Threading;

namespace AdvertisementSystem
{
    public class Interstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private AdsSettings _settings;
        private IScoreService _scoreService;
        private ICoroutineRunService _coroutineRunner;
        private string _adUnitId;
        private bool _isAdLoaded;
        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(AdsSettings settings, IScoreService scoreService, ICoroutineRunService coroutineRunner)
        {
            _settings = settings;
            _scoreService = scoreService;
            _coroutineRunner = coroutineRunner;
            _cts = new CancellationTokenSource();
        }

        private void Awake()
        {
            if (!gameObject.activeInHierarchy)
                return;

#if UNITY_IOS
            _adUnitId = _settings.IOSInterstitialAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _settings.AndroidInterstitialAdUnitId;
#else
            _adUnitId = _settings.AndroidInterstitialAdUnitId;
#endif

            if (string.IsNullOrEmpty(_adUnitId))
                return;

            _coroutineRunner.StartCoroutine(LoadAdAsync().ToCoroutine());
        }

        public async UniTask LoadAdAsync()
        {
            if (!gameObject.activeInHierarchy || _cts.Token.IsCancellationRequested)
                return;

            if (!Advertisement.isInitialized)
                await WaitForAdsInitializationAsync(_cts.Token);

            Advertisement.Load(_adUnitId, this);
            await UniTask.Yield();
        }

        public async UniTask ShowAdAsync()
        {
            if (!gameObject.activeInHierarchy || _cts.Token.IsCancellationRequested)
                return;

            if (!_isAdLoaded)
            {
                await LoadAdAsync();
                return;
            }

            Time.timeScale = 0f;
            Advertisement.Show(_adUnitId, this);
            await UniTask.Yield();
        }

        private async UniTask WaitForAdsInitializationAsync(CancellationToken cancellationToken)
        {
            while (!Advertisement.isInitialized && !cancellationToken.IsCancellationRequested)
                await UniTask.Yield();
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            _isAdLoaded = true;
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            _isAdLoaded = false;
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            Time.timeScale = 1f;
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                _scoreService.AddScore(_settings.InterstitialRewardScore);

            _isAdLoaded = false;
            if (gameObject.activeInHierarchy)
                _coroutineRunner.StartCoroutine(LoadAdAsync().ToCoroutine());
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Time.timeScale = 1f;
            _isAdLoaded = false;
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            Time.timeScale = 1f;
        }
    }
}