using AdvertisementSystem.Static_Data;
using Cysharp.Threading.Tasks;
using Common.Coroutines;
using Gameplay.Scoring;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace AdvertisementSystem
{
    public class Interstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private AdsSettings _settings;
        private IScoreService _scoreService;
        private ICoroutineRunService _coroutineRunner;
        private string _adUnitId;
        private bool _isAdLoaded;

        [Inject]
        public void Construct(AdsSettings settings, IScoreService scoreService, ICoroutineRunService coroutineRunner)
        {
            _settings = settings;
            _scoreService = scoreService;
            _coroutineRunner = coroutineRunner;
        }

        private void Awake()
        {
#if UNITY_IOS
            _adUnitId = _settings.IOSInterstitialAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _settings.AndroidInterstitialAdUnitId;
#else
            _adUnitId = _settings.AndroidInterstitialAdUnitId;
#endif

            if (string.IsNullOrEmpty(_adUnitId))
            {
                return;
            }

            _coroutineRunner.StartCoroutine(LoadAdAsync().ToCoroutine());
        }

        public async UniTask LoadAdAsync()
        {
            if (!Advertisement.isInitialized)
            {
                await WaitForAdsInitializationAsync();
            }

            Advertisement.Load(_adUnitId, this);
            await UniTask.Yield();
        }

        public async UniTask ShowAdAsync()
        {
            if (!_isAdLoaded)
            {
                await LoadAdAsync();
                return;
            }

            Advertisement.Show(_adUnitId, this);
            await UniTask.Yield();
        }

        private async UniTask WaitForAdsInitializationAsync()
        {
            while (!Advertisement.isInitialized)
            {
                await UniTask.Yield();
            }
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
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                _scoreService.AddScore(_settings.InterstitialRewardScore);
            }

            _isAdLoaded = false;
            _coroutineRunner.StartCoroutine(LoadAdAsync().ToCoroutine());
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            _isAdLoaded = false;
        }

        public void OnUnityAdsShowStart(string adUnitId) => Debug.Log($"Interstitial: Ad {adUnitId} started.");
        public void OnUnityAdsShowClick(string adUnitId) => Debug.Log($"Interstitial: Ad {adUnitId} clicked.");
    }
}