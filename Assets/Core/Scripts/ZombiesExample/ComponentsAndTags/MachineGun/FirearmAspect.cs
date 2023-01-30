using Unity.Entities;
using UnityEngine;

namespace Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun
{
    public readonly partial struct FirearmAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<BrainGunProperties> brainGunProperties;

        public void PerformShoot()
        {
            Debug.Log("shoot");
        }
        
        public bool CheckTimer()
        {
            if (EstimatedTime < 0)
            {
                EstimatedTime = brainGunProperties.ValueRO.MaxTimer;
                return true;
            }

            return false;
        }
        
        public float EstimatedTime
        {
            get => brainGunProperties.ValueRO.Timer;
            set => brainGunProperties.ValueRW.Timer = value;
        }

        public Entity BulletPrefab => brainGunProperties.ValueRO.BulletEntity;
    }
}