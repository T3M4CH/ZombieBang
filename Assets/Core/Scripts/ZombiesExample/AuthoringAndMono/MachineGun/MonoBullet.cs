using Core.Scripts.ZombiesExample.ComponentsAndTags.MachineGun;
using Unity.Entities;
using UnityEngine;

namespace Core.Scripts.ZombiesExample.AuthoringAndMono.MachineGun
{
    public class MonoBullet : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }
    }

    public class BulletBaker : Baker<MonoBullet>
    {
        public override void Bake(MonoBullet authoring)
        {
            AddComponent(new BulletProperties
            {
                Speed = authoring.Speed
            });
        }
    }
}