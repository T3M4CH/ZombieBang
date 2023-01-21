using Unity.Entities;
using Unity.Mathematics;

namespace Core.Scripts
{
    public struct RandomComponent : IComponentData
    {
        public Random Random;
    }
}
