using Cysharp.Threading.Tasks;
using Common.Coroutines;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Threading;

namespace AdvertisementSystem
{
    public class AdButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;

        private Interstitial _interstitial;
        private ICoroutineRunService _coroutineRunner;
        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(Interstitial interstitial, ICoroutineRunService coroutineRunner)
        {
            _interstitial = interstitial;
            _coroutineRunner = coroutineRunner;
            _cts = new CancellationTokenSource();
        }

        private void Awake()
        {
            if (_showAdButton == null)
                return;

            _showAdButton.onClick.AddListener(OnShowAdButtonClicked);
        }

        private void OnShowAdButtonClicked()
        {
            if (!gameObject.activeInHierarchy || _cts.Token.IsCancellationRequested)
                return;

            _coroutineRunner.StartCoroutine(_interstitial.ShowAdAsync().ToCoroutine());
        }

        private void OnDestroy()
        {
            if (_showAdButton != null)
                _showAdButton.onClick.RemoveListener(OnShowAdButtonClicked);

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}