using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Core.Scripts.ZombiesExample.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(SpawnZombieSystem))]
    public partial struct ZombieRiseSystem : ISystem
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

            new ZombieRiseJob()
            {
                DeltaTime = deltaTime,
                EntityCommandBuffer = ecb
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ZombieRiseJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

        [BurstCompile]
        private void Execute(ZombieRiseAspect zombie, [EntityIndexInQuery] int sortKey)
        {
            zombie.Rise(DeltaTime);

            if (!zombie.IsAboveGround) return;
            zombie.SetGroundLevel();
            
            EntityCommandBuffer.RemoveComponent<ZombieRiseRate>(sortKey, zombie.Entity);
            EntityCommandBuffer.SetComponentEnabled<ZombieWalkProperties>(sortKey, zombie.Entity, true);
        }
    }
}