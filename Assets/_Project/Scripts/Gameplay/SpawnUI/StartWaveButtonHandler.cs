using Gameplay.Enemies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.SpawnUI
{
    public class StartWaveButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button _startWaveButton;

        private SpawnSystem _spawnSystem;

        [Inject]
        public void Construct(SpawnSystem spawnSystem)
        {
            _spawnSystem = spawnSystem;
        }

        private void Awake()
        {
            _startWaveButton.onClick.AddListener(OnStartWaveButtonClicked);
        }

        private void OnStartWaveButtonClicked()
        {
            _spawnSystem.StartWaveSpawning();
            _startWaveButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _startWaveButton.onClick.RemoveListener(OnStartWaveButtonClicked);
        }
    }
}