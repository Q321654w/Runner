using System.Collections.Generic;
using Features.Barriers;
using Features.Barriers.Factories;
using Features.Common;
using Features.Cubes;
using Features.Cubes.Factories;
using Features.Games;
using Features.GameUpdate;
using Features.GameUpdate.Features.GameUpdate;
using Features.LevelGenerators;
using Features.LevelGenerators.CellTypes;
using Features.Maps;
using Features.Maps.Finishes;
using Features.Markers;
using Features.Players;
using Features.Scores;
using Features.Views;
using Inputs.Unity.Axis.Mouses;
using Movements.Common;
using Movements.DeltaPositions;
using Movements.DeltaPositions.Composites;
using Movements.DeltaPositions.Decorators.Implementation.Constant;
using Movements.DeltaPositions.Decorators.Implementation.Fluid;
using Movements.DeltaPositions.Split.Dimension1;
using Movements.DeltaPositions.ValueToDeltaPosition.Dimension1;
using UnityEngine;
using Update;
using Values;

namespace Features.Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private GameUpdates _gameUpdates;
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraTransform _cameraTransform;

        [SerializeField] private Map _map;
        [SerializeField] private LevelGeneratorConfig generatorConfig;

        [SerializeField] private PlayerStartPosition _startPosition;

        [SerializeField] private Cube _defaultCubePrefab;
        [SerializeField] private Cube _cubeWithParentPrefab;
        [SerializeField] private Barrier _barrierPrefab;

        [SerializeField] private float _zSpeed;
        [SerializeField] private float _xSpeed;
        [SerializeField] private EndGameView _viewPrefab;

        private void Start()
        {
            Install();
        }

        private void Install()
        {
            var deltaTime = CreateDeltaTime();
            var cubeDelta = CreateCubeDelta(deltaTime);
            var playerDelta = CreatePlayerDelta(deltaTime);
            var cubeFactory = new CubeFactory(_defaultCubePrefab, cubeDelta, _cubeWithParentPrefab);
            var player = CreatePlayer(cubeFactory, playerDelta, deltaTime);

            var barrierFactory = new BarrierFactory(_barrierPrefab);
            var levelGenerator = CreateLevelGenerator(barrierFactory, cubeFactory);
            levelGenerator.Handle(generatorConfig);

            var game = CreateGame(player);
            game.Start();
        }

        private LevelGenerator CreateLevelGenerator(BarrierFactory barrierFactory, CubeFactory cubeFactory)
        {
            var cubeGenerator = new DefaultGenerator<Cube>(new EmptyHandler<CellType[,]>(),
                new DefaultCubeFactoryProxy(cubeFactory, false), _map, CellType.Cube);

            var barrierGenerator = new DefaultGenerator<Barrier>(cubeGenerator, barrierFactory, _map, CellType.Barrier);
            var generator = new LevelGenerator(barrierGenerator);
            return generator;
        }

        private IDeltaPosition CreatePlayerDelta(IValue<float> deltaTime)
        {
            var delta = new ConstantMultiplyDeltaPosition(
                new FluidMultiplyDeltaPosition(
                    new ConstantDeltaPosition(new Vector3(0, 0, 1)), deltaTime), _zSpeed);

            return delta;
        }

        private IValue<float> CreateDeltaTime()
        {
            var deltaTime = new UnityDeltaTime();
            return deltaTime;
        }

        private Game CreateGame(Player player)
        {
            var score = new Score(player);
            var finish = new Finish(player.transform, _map.FinishMarker);

            var endGameView = Instantiate(_viewPrefab);
            endGameView.Initialize(score);
            endGameView.Hide();

            _gameUpdates.AddToUpdateList(finish);

            var game = new Game(player, _gameUpdates, score, finish, endGameView);
            return game;
        }

        private Player CreatePlayer(CubeFactory cubeFactory, IDeltaPosition delta, IValue<float> deltaTime)
        {
            var cube = cubeFactory.Create(true);
            _startPosition.MoveToMe(cube.transform);

            var cubes = new List<Cube> {cube};

            var followDelta = new ApplyPositionComposite(new[]
            {
                delta,
                new SmoothStashDelta(
                    new ZSplit(
                        new FollowForNearest<Cube>(cubes, delta)),
                    deltaTime,
                    1)
            });

            var player = Instantiate(_playerPrefab);
            player.Initialize(cubes, followDelta);
            _startPosition.MoveToMe(player.transform);

            _cameraTransform.MoveToMe(_camera.transform);
            _camera.transform.parent = player.transform;

            _gameUpdates.AddToUpdateList(new UpdateProxy(player));

            return player;
        }

        private IDeltaPosition CreateCubeDelta(IValue<float> deltaTime)
        {
            var input = new UnClampedMouseAxis(new Vector3(1, 0, 0));
            var proxy = new CacheDeltaPositionWhile(new ToXDeltaPosition(input),
                new TimerPredicate(new Timer(0.01f, false, false), deltaTime));
            
            _gameUpdates.AddToUpdateList(proxy);

            var xDelta = new ConstantMultiplyDeltaPosition(proxy, _xSpeed);

            var delta = new ConstantMultiplyDeltaPosition(
                new FluidMultiplyDeltaPosition(
                    new FluidApplyDeltaPosition(
                        new ConstantDeltaPosition(
                            new Vector3(0, 0, 1)), xDelta), deltaTime), _zSpeed);
            return delta;
        }
    }
}