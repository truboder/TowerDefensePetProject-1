using Gameplay.PlayerCastle.StaticData;
using UnityEngine;
using Utils;
using Zenject;

namespace Gameplay.PlayerCastle.Factory
{
    public class CastleFactory : ICastleFactory
    {
        private readonly DiContainer _container;
        private readonly CastleSettings _settings;
        private readonly ComponentPool<Castle> _pool;
        
        public CastleFactory(DiContainer container, CastleSettings settings)
        {
            _container = container;
            _settings = settings;
            _pool = new ComponentPool<Castle>(_settings.CastlePrefab, _container);
        }

        public Castle Create(Vector3 position)
        {
            Castle castle = _pool.Get();
            castle.transform.position = position;
            castle.Initialize(new Health(_settings.MaxHealth));
            return castle;
        }

        public void Return(Castle castle)
        {
            _pool.Return(castle);
        }
    }
}