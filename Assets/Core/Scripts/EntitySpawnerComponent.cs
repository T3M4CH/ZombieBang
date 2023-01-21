using Unity.Entities;
using UnityEngine;

namespace Core.Scripts
{
    public struct EntitySpawnerComponent : IComponentData
    {
        public Entity PlayerPrefab;
    }
}
