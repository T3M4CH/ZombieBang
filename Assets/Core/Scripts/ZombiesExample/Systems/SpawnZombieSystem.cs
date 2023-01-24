using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Entities;
using Unity.Burst;

namespace Core.Scripts.ZombiesExample.Systems
{
    [BurstCompile]
    public partial struct SpawnZombieSystem : ISystem
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
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            new SpawnZombieJob
            {
                DeltaTime = deltaTime,
                Ecb = ecbSingleton
            }.Run();
        }
    }

    [BurstCompile]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;

        private void Execute(GraveyardAspect graveyard)
        {
            graveyard.ZombieSpawnTimer -= DeltaTime;
            if (!graveyard.TimeToSpawnZombie) return;
            
            graveyard.ZombieSpawnTimer = graveyard.ZombieSpawnRate;
            var newZombie = Ecb.Instantiate(graveyard.ZombiePrefab);
        }
    }
}