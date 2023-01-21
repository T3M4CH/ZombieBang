using Unity.Entities;
using UnityEngine;

namespace Core.Scripts
{
    public partial class EntitySpawnerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityQuery = EntityManager.CreateEntityQuery(typeof(PlayerTag));

            var entitySpawnerComponent = SystemAPI.GetSingleton<EntitySpawnerComponent>();
            RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

            int spawnAmount = 1000;

            var entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(World.Unmanaged);
            if (entityQuery.CalculateEntityCount() < spawnAmount)
            {
                var entity = entityCommandBuffer.Instantiate(entitySpawnerComponent.PlayerPrefab);
                
                entityCommandBuffer.SetComponent(entity, new Speed
                {
                    Value = randomComponent.ValueRW.Random.NextFloat(1f, 5f)
                });
            }
        }
    }
}
