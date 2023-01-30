using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Core.Scripts.ZombiesExample.Systems
{
    [BurstCompile]
    public partial struct ZombieWalkSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            var brainEntity = SystemAPI.GetSingletonEntity<BrainTag>();
            var brainScale = SystemAPI.GetComponent<WorldTransform>(brainEntity).Scale;
            var brainRadius = brainScale * 11f;

            new ZombieWalkJob
            {
                DeltaTime = deltaTime,
                BrainRadius = brainRadius * brainRadius,
                ecb = ecb
            }.ScheduleParallel();
        }

        public partial struct ZombieWalkJob : IJobEntity
        {
            public float DeltaTime;
            public float BrainRadius;
            public EntityCommandBuffer.ParallelWriter ecb;

            [BurstCompile]
            private void Execute(ZombieWalkAspect zombie, [EntityIndexInQuery] int sortKey)
            {
                zombie.Walk(DeltaTime);
                if (zombie.IsInStoppingRange(float3.zero, math.pow(11, 2)))
                {
                    ecb.SetComponentEnabled<ZombieWalkProperties>(sortKey, zombie.Entity, false);
                    ecb.SetComponentEnabled<ZombieEatProperties>(sortKey, zombie.Entity, true);
                }
            }
        }
    }
}