using Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun;
using Unity.Burst;
using Unity.Entities;

namespace Core.Scripts.ZombiesExample.Systems.Firearm
{
    [BurstCompile]
    public partial struct FlyBulletSystem : ISystem
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

            var deltaTime = SystemAPI.Time.DeltaTime;

            new BulletJob
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct BulletJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(BulletAspect bulletAspect)
        {
            bulletAspect.Move(DeltaTime);
        }
    }
}