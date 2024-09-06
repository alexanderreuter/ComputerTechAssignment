using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct BulletSpawningAspect: IAspect
{
    public readonly RefRO<BulletSpawnDataComponent> spawnData;
    public readonly RefRW<FloatComponent> nextShootTime;
    public readonly RefRO<InputComponent> input;
    public readonly RefRO<LocalTransform> transform;
    
    public void HandleShooting(double elapsedTime, ref EntityCommandBuffer ecb)
    {
        if (input.ValueRO.isShooting && nextShootTime.ValueRO.value < elapsedTime)
        {
            Entity bulletEntity = ecb.Instantiate(spawnData.ValueRO.prefab);
            
            LocalTransform bulletTransform;
            bulletTransform.Position = transform.ValueRO.Position + spawnData.ValueRO.bulletSpawnPos;
            bulletTransform.Rotation = transform.ValueRO.Rotation;
            bulletTransform.Scale = 0.1f;
            ecb.SetComponent<LocalTransform>(bulletEntity, bulletTransform);
            
            nextShootTime.ValueRW.value = (float)elapsedTime + spawnData.ValueRO.shootCooldown;
        }
    }
}