using AdvertisementSystem.Static_Data;
using Cysharp.Threading.Tasks;
using Common.Coroutines;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace AdvertisementSystem
{
    public class Banner : MonoBehaviour
    {
        private AdsSettings _settings;
        private ICoroutineRunService _coroutineRunner;
        private string _adUnitId;
        private bool _isBannerLoaded;

        [Inject]
        public void Construct(AdsSettings settings, ICoroutineRunService coroutineRunner)
        {
            _settings = settings;
            _coroutineRunner = coroutineRunner;
        }

        private void Awake()
        {
#if UNITY_IOS
            _adUnitId = _settings.IOSBannerAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _settings.AndroidBannerAdUnitId;
#else
            _adUnitId = _settings.AndroidBannerAdUnitId;
#endif

            if (string.IsNullOrEmpty(_adUnitId))
            {
                return;
            }

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            _coroutineRunner.StartCoroutine(LoadBannerAsync().ToCoroutine());
        }

        private async UniTask LoadBannerAsync()
        {
            if (!Advertisement.isInitialized)
            {
                await WaitForAdsInitializationAsync();
            }

            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            Advertisement.Banner.Load(_adUnitId, options);
            await UniTask.Yield();
        }

        private async UniTask WaitForAdsInitializationAsync()
        {
            while (!Advertisement.isInitialized)
            {
                await UniTask.Yield();
            }
        }

        private void OnBannerLoaded()
        {
            _isBannerLoaded = true;
            ShowBanner();
        }

        private void OnBannerError(string message)
        {
            _isBannerLoaded = false;
        }

        public void ShowBanner()
        {
            if (!_isBannerLoaded)
            {
                return;
            }

            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                showCallback = OnBannerShown,
                hideCallback = OnBannerHidden
            };

            Advertisement.Banner.Show(_adUnitId, options);
        }

        public void HideBanner()
        {
            Advertisement.Banner.Hide();
            _isBannerLoaded = false;
        }

        private void OnBannerClicked() => Debug.Log("Banner: Banner clicked.");
        private void OnBannerShown() => Debug.Log("Banner: Banner shown callback.");
        private void OnBannerHidden() => Debug.Log("Banner: Banner hidden callback.");

        private void OnDestroy()
        {
            HideBanner();
        }
    }
}