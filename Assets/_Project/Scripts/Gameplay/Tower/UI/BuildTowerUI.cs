using Gameplay.Scoring;
using Gameplay.Tower.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Tower.UI
{
    public class BuildTowerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Button _buildButton;
        [SerializeField] private Button _closeButton;

        private TowerPlatform _currentPlatform;
        private IScoreService _scoreService;
        private TowerSettings _towerSettings;

        [Inject]
        public void Construct(IScoreService scoreService, TowerSettings towerSettings)
        {
            _scoreService = scoreService;
            _towerSettings = towerSettings;
        }

        private void Awake()
        {
            _buildButton.onClick.AddListener(OnBuildButtonClicked);
            
            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(Close);
            }

            gameObject.SetActive(false);
        }

        public void Show(TowerPlatform platform)
        {
            _currentPlatform = platform;
            _costText.text = $"Cost: {_towerSettings.TowerCost}";
            _buildButton.interactable = _scoreService.CurrentScore >= _towerSettings.TowerCost;
            gameObject.SetActive(true);
        }

        private void OnBuildButtonClicked()
        {
            if (_currentPlatform != null && _scoreService.TrySpendScore(_towerSettings.TowerCost))
            {
                _currentPlatform.BuildTower();
                Close();
            }
        }

        private void Close()
        {
            _currentPlatform = null;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _buildButton.onClick.RemoveListener(OnBuildButtonClicked);

            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveListener(Close);
            }
        }
    }
}