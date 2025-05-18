using Gameplay.Enemies;
using Gameplay.Levels;
using Gameplay.PlayerCastle.Factory;
using Gameplay.PlayerCastle.StaticData;
using UnityEngine;
using Zenject;

namespace Gameplay.PlayerCastle
{
    public class CastleSystem : IInitializable
    {
        private readonly CastleFactory _castleFactory;
        private readonly ILevelDataService _levelDataService;
        private readonly CastleSettings _settings;

        public CastleSystem(CastleFactory castleFactory, ILevelDataService levelDataService, CastleSettings settings)
        {
            _castleFactory = castleFactory;
            _levelDataService = levelDataService;
            _settings = settings;
        }

        public void Initialize()
        {
            Path enemyPath = _levelDataService.GetEnemyPath();
            
            var waypoints = enemyPath.GetWaypointsPositions();
            
            Vector3 castlePosition = waypoints[waypoints.Count - 1];
            
            Castle castle = _castleFactory.Create(castlePosition);
            _levelDataService.SetCastle(castle);
        }
    }
}