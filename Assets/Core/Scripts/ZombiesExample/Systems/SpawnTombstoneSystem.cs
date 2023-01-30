using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Core.Scripts.ZombiesExample.Systems
{
    [BurstCompile, UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyard = SystemAPI.GetAspectRW<GraveyardAspect>(graveyardEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var spawnPoints = new NativeList<float3>(Allocator.Temp);
            var tombstoneOffset = new float3(0, -2, -1);
            
            for (var i = 0; i < graveyard.NumberTombstonesToSpawn; i++)
            {
                var newTombstone = ecb.Instantiate(graveyard.TombstonePrefab);
                var newTombstoneTransform = graveyard.GetRandomTombstoneTransform();
                ecb.SetComponent(newTombstone, new LocalTransform
                {
                    Position = newTombstoneTransform._Position,
                    Rotation = newTombstoneTransform._Rotation,
                    Scale = newTombstoneTransform._Scale,
                });
                var newZombieSpawnPoint = newTombstoneTransform.Position + tombstoneOffset;
                spawnPoints.Add(newZombieSpawnPoint);
            }

            graveyard.ZombieSpawnPoints = spawnPoints.ToArray(Allocator.Persistent);

            ecb.Playback(state.EntityManager);
        }
    }
}