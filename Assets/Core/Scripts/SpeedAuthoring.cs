using Unity.Entities;
using UnityEngine;

namespace Core.Scripts
{
    public class SpeedAuthoring : MonoBehaviour
    {
        public float speedValue;
    }

    public class SpeedBaker : Baker<SpeedAuthoring>
    {
        public override void Bake(SpeedAuthoring authoring)
        {
            AddComponent(new Speed
            {
                Value = authoring.speedValue
            });
        }
    }
}