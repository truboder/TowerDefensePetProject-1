using AdvertisementSystem.Static_Data;
using Cysharp.Threading.Tasks;
using Common.Coroutines;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace AdvertisementSystem
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private AdsSettings _settings;
        private ICoroutineRunService _coroutineRunner;
        private string _gameId;

        [Inject]
        public void Construct(AdsSettings settings, ICoroutineRunService coroutineRunner)
        {
            _settings = settings;
            _coroutineRunner = coroutineRunner;
        }

        private void Awake()
        {
            _coroutineRunner.StartCoroutine(InitializeAdsAsync().ToCoroutine());
        }

        private async UniTask InitializeAdsAsync()
        {
#if UNITY_IOS
            _gameId = _settings.IOSGameId;
#elif UNITY_ANDROID
            _gameId = _settings.AndroidGameId;
#elif UNITY_EDITOR
            _gameId = _settings.AndroidGameId;
#endif

            if (string.IsNullOrEmpty(_gameId))
            {
                return;
            }

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _settings.TestMode, this);
                await WaitForInitializationAsync();
            }
        }

        private async UniTask WaitForInitializationAsync()
        {
            while (!Advertisement.isInitialized)
            {
                await UniTask.Yield();
            }
        }

        public void OnInitializationComplete()
        {

        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {

        }
    }
}