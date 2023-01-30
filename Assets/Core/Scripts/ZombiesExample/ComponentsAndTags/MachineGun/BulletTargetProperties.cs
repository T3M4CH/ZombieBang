using Unity.Entities;
using Unity.Transforms;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun
{
    public  struct BulletTargetProperties : IComponentData
    {
        public ZombieWalkAspect ZombieWalkAspect;
    }
}