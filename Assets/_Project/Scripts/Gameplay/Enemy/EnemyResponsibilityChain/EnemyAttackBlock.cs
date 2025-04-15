using UnityEngine;
using Zenject;

public class EnemyAttackBlock : EnemyBehaviourBlock
{
    private PlayerHealthService _healthService;

    [Inject]
    public void Construct(PlayerHealthService healthService)
    {
        _healthService = healthService;
    }

    public override void Run()
    {
        _healthService.TakeDamage(1);
        _nextBlock?.Run();
    }
}
