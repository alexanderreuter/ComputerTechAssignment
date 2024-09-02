using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct PlayerMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTagComponent>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        foreach (var playerMovementAspect in SystemAPI.Query<PlayerMovementAspect>())
        {
            playerMovementAspect.HandleMovement(SystemAPI.Time.DeltaTime);
        }
    }
}
