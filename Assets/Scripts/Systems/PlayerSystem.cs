using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct PlayerSystem : ISystem
{
    private EntityManager entityManager;
    
    public void OnCreate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        state.RequireForUpdate<PlayerTagComponent>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        foreach (var playerAspect in SystemAPI.Query<PlayerAspect>())
        {
            playerAspect.HandleMovement(SystemAPI.Time.DeltaTime);
            playerAspect.HandleShooting(SystemAPI.Time.ElapsedTime, entityManager);
        }
    }
}
