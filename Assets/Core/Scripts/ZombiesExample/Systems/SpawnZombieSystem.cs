using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Core.Scripts.ZombiesExample.Extensions;
using Unity.Entities;
using Unity.Burst;
using Unity.Physics;
using Unity.Transforms;

namespace Core.Scripts.ZombiesExample.Systems
{
    public partial struct SpawnZombieSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            state.CompleteDependency();
            
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

            var newZombieTransform = graveyard.GetZombieSpawnPoint();
            Ecb.SetComponent(newZombie, new LocalTransform
            {
                Position = newZombieTransform.Position,
                Rotation = newZombieTransform.Rotation,
                Scale = newZombieTransform.Scale,
            });

            var zombieHeading = MathHelpers.GetHeading(newZombieTransform.Position, graveyard.Position);
            Ecb.SetComponent(newZombie, new ZombieHeading
            {
                Value = zombieHeading
            });
        }
    }
}