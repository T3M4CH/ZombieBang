using Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun;
using Unity.Entities;
using UnityEngine;

namespace Core.Scripts.ZombiesExample.AuthoringAndMono.MachineGun
{
    public class MonoFirearm : MonoBehaviour
    {
        [field:SerializeField] public float Cooldown { get; private set; }
        [field:SerializeField] public GameObject BulletPrefab { get; private set; }
    }

    public class FirearmBaker : Baker<MonoFirearm>
    {
        public override void Bake(MonoFirearm authoring)
        {
            AddComponent(new BrainGunProperties
            {
                MaxTimer = authoring.Cooldown,
                BulletEntity = GetEntity(authoring.BulletPrefab),
            });
        }
    }
}