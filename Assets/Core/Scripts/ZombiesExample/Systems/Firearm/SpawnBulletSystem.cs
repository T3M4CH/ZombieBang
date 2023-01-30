using System;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Transforms;
using UnityEngine;

namespace Core.Scripts.ZombiesExample.Systems.Firearm
{
    [BurstCompile]
    public partial struct SpawnBulletSystem : ISystem
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
                .CreateCommandBuffer(state.WorldUnmanaged);

            var zombiesList = new List<ZombieWalkAspect>();

            foreach (var walkAspect in SystemAPI.Query<ZombieWalkAspect>().WithNone<TargetTag>())
            {
                zombiesList.Add(walkAspect);
            }

            if (zombiesList.Count <= 0) return;

            var zombieWalkAspect = zombiesList.First();

            new SpawnJob()
            {
                DeltaTime = deltaTime,
                ZombieWalkAspect = zombieWalkAspect,
                EntityCommandBuffer = ecb,
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct SpawnJob : IJobEntity
    {
        public float DeltaTime;
        [NativeDisableUnsafePtrRestriction] public ZombieWalkAspect ZombieWalkAspect;
        public EntityCommandBuffer EntityCommandBuffer;

        [BurstCompile]
        private void Execute(FirearmAspect firearmAspect)
        {
            firearmAspect.EstimatedTime -= DeltaTime;

            if (!firearmAspect.CheckTimer()) return;

            var bulletEntity = EntityCommandBuffer.Instantiate(firearmAspect.BulletPrefab);
            
            EntityCommandBuffer.AddComponent(bulletEntity, new BulletTargetProperties
            {
                ZombieWalkAspect = ZombieWalkAspect
            });
            
            EntityCommandBuffer.AddComponent(ZombieWalkAspect.Entity, new TargetTag());
        }
    }
}