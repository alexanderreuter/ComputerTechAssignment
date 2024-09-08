using Unity.Entities;

public partial struct BulletSpawningSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTagComponent>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.TryGetSingleton<InputComponent>(out var input) || !input.isShooting)
            return;
        
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        var handle = new BulletSpawnJob
        {
            elapsedTime = SystemAPI.Time.ElapsedTime,
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
        }.ScheduleParallel(state.Dependency);

        state.Dependency = handle;
    }
    
    public partial struct BulletSpawnJob : IJobEntity
    {
        public double elapsedTime;
        public EntityCommandBuffer.ParallelWriter ecb;

        private void Execute(BulletSpawningAspect bulletspawningAspect, [EntityIndexInQuery] int entityIndex)
        {
            bulletspawningAspect.HandleShooting(elapsedTime, ref ecb, entityIndex);
        }
    }
}

