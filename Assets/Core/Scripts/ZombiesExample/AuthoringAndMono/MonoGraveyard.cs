using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Core.Scripts.ZombiesExample.AuthoringAndMono
{
    public class MonoGraveyard : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public GameObject TombstonePrefab;
        public uint RandomSeed;
        public GameObject ZombiePrefab;
        public float ZombieSpawnRate;
    }
    
    public class GraveyardBaker : Baker<MonoGraveyard>
    {
        public override void Bake(MonoGraveyard authoring)
        {
            AddComponent(new GraveyardProperties
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab),
                ZombiePrefab = GetEntity(authoring.ZombiePrefab),
                ZombieSpawnRate = authoring.ZombieSpawnRate,
            });
            AddComponent(new GraveyardRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<ZombieSpawnPoints>();
            AddComponent<ZombieSpawnTimer>();
        }
    }
}