using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace Core.Scripts.ZombiesExample.AuthoringAndMono
{
    public class MonoZombie : MonoBehaviour
    {
        public float RiseRate;
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;

        public float EatDamage;
        public float EatAmplitude;
        public float EatFrequency;
    }

    public class ZombieBaker : Baker<MonoZombie>
    {
        public override void Bake(MonoZombie authoring)
        {
            AddComponent(new ZombieRiseRate { Value = authoring.RiseRate });
            AddComponent(new ZombieWalkProperties
            {
                WalkSpeed = authoring.WalkSpeed,
                WalkAmplitude = authoring.WalkAmplitude,
                WalkFrequency = authoring.WalkFrequency
            });
            
            AddComponent(new ZombieEatProperties
            {
                EatAmplitude = authoring.EatAmplitude,
                EatDamagePerSecond = authoring.EatDamage,
                EatFrequency = authoring.EatFrequency,
            });
            AddComponent<ZombieTimer>();
            AddComponent<ZombieHeading>();
            AddComponent<NewZombieTag>();
        }
    }
}