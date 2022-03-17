using Features.Cubes;
using Features.Cubes.Factories;

namespace Features.Common
{
    internal class DefaultCubeFactoryProxy : IFactory<Cube>
    {
        private readonly CubeFactory _cubeFactory;
        private readonly bool _isParent;

        public DefaultCubeFactoryProxy(CubeFactory cubeFactory, bool isParent)
        {
            _cubeFactory = cubeFactory;
            _isParent = isParent;
        }

        public Cube Create()
        {
            return _cubeFactory.Create(_isParent);
        }
    }
}