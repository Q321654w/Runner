using Features.Cubes;
using Update;

namespace Features.GameUpdate
{
    internal class UpdateProxy : IUpdate
    {
        private readonly IGameUpdate _gameUpdate;
        
        public UpdateProxy(IGameUpdate gameUpdate)
        {
            _gameUpdate = gameUpdate;
        }

        public void Update()
        {
            _gameUpdate.GameUpdate();
        }
    }
}