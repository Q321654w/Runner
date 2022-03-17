using Features.Common;
using Features.LevelGenerators.CellTypes;
using UnityEngine;

namespace Features.LevelGenerators
{
    public class LevelGenerator : IHandler<LevelGeneratorConfig>
    {
        private readonly IHandler<CellType[,]> _nextHandler;

        public LevelGenerator(IHandler<CellType[,]> nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public void Handle(LevelGeneratorConfig config)
        {
            var size = config.Size;
            var cells = new CellType[size.x, size.y];

            var offset = config.StartOffset;
            var startPosition = offset;

            for (int y = startPosition.y; y < size.y; y++)
            {
                for (int x = startPosition.x; x < size.x; x++)
                {
                    var type = RandomizeType(config);
                    cells[x, y] = type;

                    var coordinateOffset = CalculateOffset(x, y, config, size,type);
                    x += coordinateOffset.x;
                    y += coordinateOffset.y;
                }
            }

            _nextHandler.Handle(cells);
        }

        private Vector2Int CalculateOffset(int x, int y, LevelGeneratorConfig config, Vector2Int size, CellType type)
        {
            if (type == CellType.Empty)
                return Vector2Int.zero;
            
            if (x + config.MinContentDistance.x <= size.x)
                return new Vector2Int(config.MinContentDistance.x, 0);

            return new Vector2Int(config.MinContentDistance.x, size.x - x);
        }

        private CellType RandomizeType(LevelGeneratorConfig config)
        {
            var value = Random.value;
            return config.GetCellType(value);
        }
    }
}