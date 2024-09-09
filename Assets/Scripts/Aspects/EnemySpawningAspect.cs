using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public readonly partial struct EnemySpawningAspect : IAspect
{
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRO<EnemySpawnDataComponent> spawnData;
    public readonly RefRW<FloatComponent> timeToNextSpawn;

    public void HandleSpawning(float deltaTime, ref EntityCommandBuffer.ParallelWriter ecb, int entityIndex, Random rng)
    {
        timeToNextSpawn.ValueRW.value -= deltaTime;

        if (timeToNextSpawn.ValueRO.value <= 0.0f)
        {
            Entity enemyEntity = ecb.Instantiate(entityIndex, spawnData.ValueRO.prefab);
            
            LocalTransform enemyTransform;
            enemyTransform.Position = new (rng.NextFloat(-17f, 17f), rng.NextFloat(15f, 20f), 0f);
            enemyTransform.Rotation = transform.ValueRO.Rotation;
            enemyTransform.Scale = 1f;
            ecb.SetComponent<LocalTransform>(entityIndex, enemyEntity, enemyTransform);
            
            timeToNextSpawn.ValueRW.value = spawnData.ValueRO.spawnInterval;
        }
    }
}


