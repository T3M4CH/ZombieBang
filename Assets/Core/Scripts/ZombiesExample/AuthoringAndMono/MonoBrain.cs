using Core.Scripts.ZombiesExample.ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace Core.Scripts.ZombiesExample.AuthoringAndMono
{
    public class MonoBrain : MonoBehaviour
    {
        public float Health;
    }

    public class BrainBaker : Baker<MonoBrain>
    {
        public override void Bake(MonoBrain authoring)
        {
            AddComponent<BrainTag>();
            AddComponent(new BrainHealth
            {
                Value = authoring.Health,
                Max = authoring.Health
            });
            AddBuffer<BrainDamageBufferElement>();
        }
    }
}