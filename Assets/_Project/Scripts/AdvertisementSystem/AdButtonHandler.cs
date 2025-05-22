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

        private Interstitial _interstitial;
        private ICoroutineRunService _coroutineRunner;

        [Inject]
        public void Construct(Interstitial interstitial, ICoroutineRunService coroutineRunner)
        {
            _interstitial = interstitial;
            _coroutineRunner = coroutineRunner;
        }

        private void Awake()
        {
            _showAdButton.onClick.AddListener(OnShowAdButtonClicked);
        }

        private void OnShowAdButtonClicked()
        {
            _coroutineRunner.StartCoroutine(_interstitial.ShowAdAsync().ToCoroutine());
        }

        private void OnDestroy()
        {
            if (_showAdButton != null)
            {
                _showAdButton.onClick.RemoveListener(OnShowAdButtonClicked);
            }
        }
    }
}