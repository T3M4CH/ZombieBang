using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun
{
    public readonly partial struct BulletAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<BulletProperties> _bulletProperties;
        private readonly RefRW<BulletTargetProperties> _bulletTargetProperties;

        public void Move(float deltaTime)
        {
            var direction = math.normalize(ZombieTransform.LocalPosition - _transformAspect.LocalPosition);
            _transformAspect.LocalPosition += direction * _bulletProperties.ValueRO.Speed * deltaTime;
        }

        private TransformAspect ZombieTransform => _bulletTargetProperties.ValueRO.ZombieWalkAspect.Transform;
    }
}