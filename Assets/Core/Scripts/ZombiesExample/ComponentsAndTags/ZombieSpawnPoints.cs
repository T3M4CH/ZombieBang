using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags
{
    public struct ZombieSpawnPoints : IComponentData
    {
        public NativeArray<float3> Value;
    }
}