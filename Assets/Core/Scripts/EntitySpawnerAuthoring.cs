using Unity.Entities;
using UnityEngine;

namespace Core.Scripts
{
    public class EntitySpawnerAuthoring : MonoBehaviour
    {
        public GameObject entityPrefab;
    }

    public class EntitySpawnerBaker : Baker<EntitySpawnerAuthoring>
    {
        public override void Bake(EntitySpawnerAuthoring authoring)
        {
            AddComponent(new EntitySpawnerComponent
            {
                PlayerPrefab = GetEntity(authoring.entityPrefab),
            });
        }
    }
}
