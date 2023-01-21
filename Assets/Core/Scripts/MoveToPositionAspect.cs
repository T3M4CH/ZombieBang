using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Core.Scripts
{
    public readonly partial struct MoveToPositionAspect : IAspect
    {
        private readonly Entity _entity;
        private readonly RefRO<Speed> _speed;
        private readonly TransformAspect _transformAspect;
        private readonly RefRW<TargetPosition> _targetPosition;

        public void Move(float deltaTime)
        {
            float3 direction = math.normalize(_targetPosition.ValueRW.Value - _transformAspect.LocalPosition);
            _transformAspect.LocalPosition += direction * deltaTime * _speed.ValueRO.Value;
        }

        public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent)
        {
            const float reachedTargetDistance = .5f;

            if (math.distance(_transformAspect.LocalPosition, _targetPosition.ValueRW.Value) < reachedTargetDistance)
            {
                _targetPosition.ValueRW.Value = GetRandomPosition(randomComponent);
            }
        }

        private float3 GetRandomPosition(RefRW<RandomComponent> random)
        {
            return new float3(random.ValueRW.Random.NextFloat(-5, 5), 0, random.ValueRW.Random.NextFloat(-5, 5));
        }
    }
}