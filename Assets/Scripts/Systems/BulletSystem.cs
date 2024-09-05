using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{
    // private Entity player;
    // private LocalTransform playerTransform;
    // private EntityManager entityManager;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BulletTag>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        foreach (var bulletAspect in SystemAPI.Query<BulletAspect>())
        {
            bulletAspect.HandleMovement(SystemAPI.Time.DeltaTime); 
            // HandleLifetime(ref state, bulletAspect);
        }
    }
    
    public void HandleLifetime(ref SystemState state, BulletAspect bulletAspect)
    {
        // bulletAspect.bulletData.ValueRW.lifeTime -= SystemAPI.Time.DeltaTime;
        // var ecb = new EntityCommandBuffer(Allocator.TempJob);
        //
        // if (bulletAspect.bulletData.ValueRO.lifeTime <= 0f)
        // {
        //     // ecb.DestroyEntity();
        // }

    }
}
