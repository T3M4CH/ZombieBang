using Unity.Entities;
using Unity.Mathematics;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}