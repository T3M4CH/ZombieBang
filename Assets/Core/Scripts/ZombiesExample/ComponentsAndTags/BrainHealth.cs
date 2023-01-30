using Unity.Entities;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags
{
    public struct BrainHealth : IComponentData
    {
        public float Value;
        public float Max;
    }
}