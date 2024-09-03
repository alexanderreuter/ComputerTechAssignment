using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


public partial struct BulletSystem : ISystem
{
    private Entity player;
    // private LocalTransform playerTransform;
    private EntityManager entityManager;
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTagComponent>();
        entityManager = state.EntityManager;
        player = SystemAPI.GetSingletonEntity<PlayerTagComponent>();
        // playerTransform = entityManager.GetComponentData<LocalTransform>(player);
    }
    
    public void OnUpdate(ref SystemState state)
    {
        foreach (var bulletAspect in SystemAPI.Query<BulletAspect>())
        {
            bulletAspect.HandleMovement(SystemAPI.Time.DeltaTime); 
        }
    }
}
