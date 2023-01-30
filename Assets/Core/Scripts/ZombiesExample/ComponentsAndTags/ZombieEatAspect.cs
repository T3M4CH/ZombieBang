using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags
{
    public readonly partial struct ZombieEatAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transform;
        private readonly RefRW<ZombieTimer> _zombieTimer;
        private readonly RefRO<ZombieEatProperties> _eatProperties;
        private readonly RefRO<ZombieHeading> _heading;

        public void Eat(float deltaTime, EntityCommandBuffer.ParallelWriter ecb, int sortKey, Entity brainEntity)
        {
            ZombieTimer += deltaTime;
            var eatAngle = EatAmplitude * math.sin(EatFrequency * ZombieTimer);
            _transform.LocalRotation = quaternion.Euler(eatAngle, Heading, 0);

            var eatDamage = EatDamagePerSecond * deltaTime;
            var curBrainDamage = new BrainDamageBufferElement { Value = eatDamage };
            ecb.AppendToBuffer(sortKey, brainEntity, curBrainDamage);
        }
        
        private float EatDamagePerSecond => _eatProperties.ValueRO.EatDamagePerSecond;
        private float EatAmplitude => _eatProperties.ValueRO.EatAmplitude;
        private float EatFrequency => _eatProperties.ValueRO.EatFrequency;
        private float Heading => _heading.ValueRO.Value;

        private float ZombieTimer
        {
            get => _zombieTimer.ValueRO.Value;
            set => _zombieTimer.ValueRW.Value = value;
        }
    }
}
