using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct BulletAspect : IAspect
{
    public readonly RefRW<LocalTransform> transform;
    public readonly RefRO<BulletComponent> bulletData;

    public void HandleMovement(float deltaTime)
    {
        transform.ValueRW.Position = bulletData.ValueRO.initialPosition;
    }
}
