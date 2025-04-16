using Game.Player;
using Zenject;

namespace Game.Enemy
{
    public class AttackBlock : BehaviourBlock
    {
        private HealthService _healthService;

        [Inject]
        public void Construct(HealthService healthService)
        {
            _healthService = healthService;
        }

        public override void Process()
        {
            _healthService.TakeDamage(1);
            _nextBlock?.Process();
        }
    }
}
