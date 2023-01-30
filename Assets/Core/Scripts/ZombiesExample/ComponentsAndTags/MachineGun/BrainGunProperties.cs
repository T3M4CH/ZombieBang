using Unity.Entities;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun
{
    public struct BrainGunProperties : IComponentData
    {
        public float Timer;
        public float MaxTimer;
        public Entity BulletEntity;
    }
}
