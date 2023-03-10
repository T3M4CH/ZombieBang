using Core.Scripts.ZombiesExample.Extensions;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        private const float BrainSafetyRadiusSq = 15;

        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;

        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<GraveyardRandom> _graveyardRandom;
        private readonly RefRW<ZombieSpawnPoints> _zombieSpawnPoints;
        private readonly RefRW<ZombieSpawnTimer> _zombieSpawnTimer;

        public NativeArray<float3> ZombieSpawnPoints
        {
            get => _zombieSpawnPoints.ValueRO.Value;
            set => _zombieSpawnPoints.ValueRW.Value = value;
        }

        public LocalTransform GetRandomTombstoneTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(0.5f),
            };
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;

            do
            {
                randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(_transformAspect.WorldPosition, randomPosition) <=
                     BrainSafetyRadiusSq * BrainSafetyRadiusSq);

            return randomPosition;
        }

        private float3 MinCorner => _transformAspect.WorldPosition - HalfDimensions;
        private float3 MaxCorner => _transformAspect.WorldPosition + HalfDimensions;

        private float3 HalfDimensions => new()
        {
            x = _graveyardProperties.ValueRO.FieldDimensions.x * .5f,
            y = 0,
            z = _graveyardProperties.ValueRO.FieldDimensions.y * .5f
        };

        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        private quaternion GetRandomRotation() =>
            quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));

        private float GetRandomScale(float min) => _graveyardRandom.ValueRW.Value.NextFloat(min, 1);

        public float ZombieSpawnTimer
        {
            get => _zombieSpawnTimer.ValueRO.Value;
            set => _zombieSpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnZombie => ZombieSpawnTimer <= 0f;

        public float ZombieSpawnRate => _graveyardProperties.ValueRO.ZombieSpawnRate;

        public Entity ZombiePrefab => _graveyardProperties.ValueRO.ZombiePrefab;

        public LocalTransform GetZombieSpawnPoint()
        {
            var position = GetRandomZombieSpawnPoint();
            return new LocalTransform
            {
                Position = position,
                Rotation = quaternion.RotateY(MathHelpers.GetHeading(position, _transformAspect.LocalPosition)),
                Scale = 1f
            };
        }

        private float3 GetRandomZombieSpawnPoint()
        {
            return ZombieSpawnPoints[_graveyardRandom.ValueRW.Value.NextInt(ZombieSpawnPoints.Length)];
        }

        public float3 Position => _transformAspect.WorldPosition;
    }
}