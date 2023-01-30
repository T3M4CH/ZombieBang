using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Core.Scripts.ZombiesExample.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(ZombieWalkSystem))]
    public partial struct ZombieEatSystem : ISystem
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

            new ZombieEatingJob
            {
                DeltaTime = deltaTime,
                ECB = ecb,
                BrainEntity = brainEntity
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ZombieEatingJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        public Entity BrainEntity;

        [BurstCompile]
        private void Execute(ComponentsAndTags.ZombieEatAspect zombieEatAspect, [EntityIndexInQuery] int sortKey)
        {
            zombieEatAspect.Eat(DeltaTime, ECB, sortKey, BrainEntity);
        }
    }
}