using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct PlayerMovementAspect : IAspect
{
    public readonly RefRW<LocalTransform> transform;
    public readonly RefRO<MovementComponent> movement;
    public readonly RefRO<InputComponent> input;
    
    public void HandleMovement(float deltaTime)
    {
        float3 position = transform.ValueRO.Position;
        position.x = input.ValueRO.moveInput.x;
        position.y = input.ValueRO.moveInput.y;

        transform.ValueRW.Position += position * movement.ValueRO.speed * deltaTime;
    }
}
