using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct PlayerAspect : IAspect
{
    // public readonly RefRO<PlayerTagComponent> playerTag;
    public readonly RefRW<LocalTransform> transform;
    public readonly RefRO<MovementComponent> movement;
    public readonly RefRO<InputComponent> input;
    public readonly RefRO<BulletSpawnDataComponent> bulletSpawnData;
    public readonly RefRW<FloatComponent> nextShootTime;

    public void HandleMovement(float deltaTime)
    {
        float3 position = transform.ValueRO.Position;
        position.x = input.ValueRO.moveInput.x;
        position.y = input.ValueRO.moveInput.y;

        transform.ValueRW.Position += position * movement.ValueRO.speed * deltaTime;
    }
    
    public void HandleShooting(double elapsedTime, EntityManager entityManager)
    {
        if (input.ValueRO.isShooting && nextShootTime.ValueRO.value < elapsedTime)
        {
            Entity bulletEntity = entityManager.Instantiate(bulletSpawnData.ValueRO.prefab);

            

            nextShootTime.ValueRW.value = (float)elapsedTime + bulletSpawnData.ValueRO.shootCooldown;
        }
    }
}
