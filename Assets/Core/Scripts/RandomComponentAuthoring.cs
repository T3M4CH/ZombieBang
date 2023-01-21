using Unity.Entities;
using UnityEngine;

namespace Core.Scripts
{
    public class RandomComponentAuthoring : MonoBehaviour
    {
        
    }

    public class RandomBaker : Baker<RandomComponentAuthoring>
    {
        public override void Bake(RandomComponentAuthoring authoring)
        {
            AddComponent(new RandomComponent
            {
                Random = new Unity.Mathematics.Random(1)
            });
        }
    }
}
