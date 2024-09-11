using Unity.Entities;
using Unity.Transforms;

public readonly partial struct BulletSpawningAspect: IAspect
{
    public readonly RefRO<BulletSpawnDataComponent> spawnData;
    public readonly RefRW<FloatComponent> nextShootTime;
    public readonly RefRO<InputComponent> input;
    public readonly RefRO<LocalTransform> transform;
    
    public void HandleSpawning(double elapsedTime, ref EntityCommandBuffer.ParallelWriter ecb, int entityIndex)
    {
        if (input.ValueRO.isShooting && nextShootTime.ValueRO.value < elapsedTime)
        {
            Entity bulletEntity = ecb.Instantiate(entityIndex, spawnData.ValueRO.prefab);
            
            LocalTransform bulletTransform;
            bulletTransform.Position = transform.ValueRO.Position + spawnData.ValueRO.bulletSpawnPos;
            bulletTransform.Rotation = transform.ValueRO.Rotation;
            bulletTransform.Scale = 0.1f;
            ecb.SetComponent<LocalTransform>(entityIndex, bulletEntity, bulletTransform);
            
            nextShootTime.ValueRW.value = (float)elapsedTime + spawnData.ValueRO.shootCooldown;
        }
    }
}