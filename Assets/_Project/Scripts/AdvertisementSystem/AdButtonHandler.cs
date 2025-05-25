using Cysharp.Threading.Tasks;
using Common.Coroutines;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AdvertisementSystem
{
    public class AdButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;

        private AdsService _adsService;
        private ICoroutineRunService _coroutineRunner;

        [Inject]
        public void Construct(AdsService adsService, ICoroutineRunService coroutineRunner)
        {
            _adsService = adsService;
            _coroutineRunner = coroutineRunner;
        }

        private void Awake()
        {
            if (_showAdButton != null)
                _showAdButton.onClick.AddListener(OnShowAdButtonClicked);
        }

        private void OnShowAdButtonClicked()
        {
            _coroutineRunner.StartCoroutine(_adsService.ShowInterstitialAsync().ToCoroutine());
        }

        private void OnDestroy()
        {
            if (_showAdButton != null)
                _showAdButton.onClick.RemoveListener(OnShowAdButtonClicked);
        }
    }
}