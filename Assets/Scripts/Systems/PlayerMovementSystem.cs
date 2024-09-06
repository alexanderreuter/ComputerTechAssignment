using Unity.Entities;

public partial struct PlayerMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTagComponent>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        new MoveJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        }.Schedule();
    }
    
    public partial struct MoveJob : IJobEntity
    {
        public float deltaTime;
        
        private void Execute (PlayerMovementAspect playerMovementAspect)
        {
            playerMovementAspect.HandleMovement(deltaTime);
        }
    }
}

