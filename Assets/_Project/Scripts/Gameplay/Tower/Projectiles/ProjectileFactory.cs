using Gameplay.Tower.StaticData;
using Utils;
using Zenject;

namespace Gameplay.Tower.Projectiles
{
    public class ProjectileFactory
    {
        private readonly ComponentPool<Projectile> _pool;
        private readonly TowerSettings _settings;
        private readonly DiContainer _container;

        public ProjectileFactory(DiContainer container, TowerSettings settings)
        {
            _container = container;
            _settings = settings;
            _pool = new ComponentPool<Projectile>(_settings.ProjectilePrefab, _container);
        }

        public Projectile Create()
        {
            var projectile = _pool.Get();
            projectile.OnTargetReached += Return;
            return projectile;
        }

        private void Return(Projectile projectile)
        {
            projectile.OnTargetReached -= Return;
            _pool.Return(projectile);
        }
    }
}