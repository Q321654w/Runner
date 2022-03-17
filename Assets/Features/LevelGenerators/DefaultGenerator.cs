using Features.Common;
using Features.LevelGenerators.CellTypes;
using Features.Maps;
using UnityEngine;

namespace Features.LevelGenerators
{
    public class DefaultGenerator<T> : IHandler<CellType[,]> where T : MonoBehaviour
    {
        private readonly IHandler<CellType[,]> _nextHandler;

        private readonly CellType _targetType;
        private readonly IFactory<T> _factory;
        private readonly Map _map;

        public DefaultGenerator(IHandler<CellType[,]> nextHandler,
            IFactory<T> factory, Map map, CellType targetType)
        {
            _nextHandler = nextHandler;
            _factory = factory;
            _map = map;
            _targetType = targetType;
        }

        public void Handle(CellType[,] config)
        {
            var xSize = config.GetLength(0);
            var ySize = config.GetLength(1);

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    if (config[x, y] != _targetType)
                        continue;

                    var xPercent = (float) x / xSize;
                    var yPercent = (float) y / ySize;

                    CreateObject(xPercent, yPercent);
                }
            }

            _nextHandler.Handle(config);
        }

        private void CreateObject(float xPercent, float yPercent)
        {
            var obj = _factory.Create();

            var start = _map.Start.transform.position;
            var end = _map.End.transform.position;

            var xLerp = Mathf.Lerp(start.x, end.x, xPercent);
            var yLerp = Mathf.Lerp(start.z, end.z, yPercent);

            var mapTransform = _map.transform;
            var y = mapTransform.position.y + _map.HalfScale.y + obj.transform.lossyScale.y / 2 + obj.transform.position.y;
            obj.transform.position = new Vector3(xLerp, y, yLerp);
        }
    }
}