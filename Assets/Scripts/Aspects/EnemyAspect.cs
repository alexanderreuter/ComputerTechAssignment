using Unity.Entities;
using Unity.Transforms;

public readonly partial struct EnemyAspect : IAspect
{
    public readonly RefRW<LocalTransform> transform;
    public readonly RefRW<EnemyComponent> enemyData;
    public readonly RefRW<LifetimeComponent> lifetime;

    public void HandleMovement(float deltaTime)
    {
        transform.ValueRW.Position += enemyData.ValueRO.speed * deltaTime * transform.ValueRO.Up();
    }

    public void HandleLifetime(float deltaTime, ref EntityCommandBuffer.ParallelWriter ecb, int entityIndex, Entity entity)
    {
        lifetime.ValueRW.lifetime -= deltaTime;
        
        if (lifetime.ValueRO.lifetime <= 0f)
        {
            ecb.DestroyEntity(entityIndex, entity);
        }
    }
}
