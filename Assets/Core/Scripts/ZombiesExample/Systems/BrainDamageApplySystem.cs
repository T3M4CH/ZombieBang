using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Core.Scripts.ZombiesExample.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct BrainDamageApplySystem : ISystem
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
            state.Dependency.Complete();
            
            foreach (var brainAspect in SystemAPI.Query<BrainAspect>())
            {
                brainAspect.DamageBrain();
            }
        }
    }
}