using Gameplay.Tower.StaticData;
using Utils;
using Zenject;

namespace Gameplay.Tower.ProjectileFactory
{
    public class ProjectileFactory
    {
        private readonly ComponentPool<Projectile> _pool;
        private readonly TowerSettings _settings;
        private readonly DiContainer _container;

        public ProjectileFactory(TowerSettings settings, DiContainer container)
        {
            _settings = settings;
            _container = container;
            _pool = new ComponentPool<Projectile>(_settings.ProjectilePrefab, _container);
        }

        public Projectile Create()
        {
            var projectile = _pool.Get();
            projectile.OnTargetReached += () => Return(projectile);
            return projectile;
        }

        private void Return(Projectile projectile)
        {
            projectile.OnTargetReached -= () => Return(projectile);
            _pool.Return(projectile);
        }
    }
}