using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct BulletAspect : IAspect
{
    public readonly RefRW<LocalTransform> transform;
    public readonly RefRW<BulletComponent> bulletData;

    public void HandleMovement(float deltaTime)
    {
        transform.ValueRW.Position += bulletData.ValueRO.speed * deltaTime * transform.ValueRO.Up();
    }

    public void HandleLifetime(float deltaTime, ref EntityCommandBuffer.ParallelWriter ecb, int entityIndex, Entity entity)
    {
        bulletData.ValueRW.lifeTime -= deltaTime;
        
        if (bulletData.ValueRO.lifeTime <= 0f)
        {
            ecb.DestroyEntity(entityIndex, entity);
        }
    }
}
