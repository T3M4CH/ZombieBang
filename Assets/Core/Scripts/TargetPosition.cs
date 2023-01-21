using Unity.Entities;
using Unity.Mathematics;

namespace Core.Scripts
{
    public struct TargetPosition : IComponentData
    {
        public float3 Value;
    }
}
