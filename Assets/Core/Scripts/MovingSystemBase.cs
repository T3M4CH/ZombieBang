using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Core.Scripts
{
    public partial class MovingSystemBase : SystemBase
    {
        protected override void OnUpdate()
        {
            // var randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
            //
            // foreach (var moveToPositionAspect in SystemAPI.Query<MoveToPositionAspect>())
            // {
            //     moveToPositionAspect.Move(SystemAPI.Time.DeltaTime, randomComponent);
            // }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            
        }
    }
}