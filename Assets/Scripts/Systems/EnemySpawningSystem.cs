using Unity.Entities;
using Unity.Mathematics;
using RandomUnity = UnityEngine.Random;
using Random = Unity.Mathematics.Random;

public partial struct EnemySpawningSystem : ISystem
{
    void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemySpawnerTagComponent>();
    }

    void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        
        var handle = new EnemySpawnJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            seed = (uint)RandomUnity.Range(1, 100000) // Why do you need 1 to 100 000 for it to work, helloow Unity ?!?!?
        }.ScheduleParallel(state.Dependency);

        state.Dependency = handle;
    }
    
    public partial struct EnemySpawnJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer.ParallelWriter ecb;
        public uint seed;

        private void Execute(EnemySpawningAspect enemySpawningAspect, [EntityIndexInQuery] int entityIndex)
        {
            Unity.Mathematics.Random rng = new Unity.Mathematics.Random(seed);
            enemySpawningAspect.HandleSpawning(deltaTime, ref ecb, entityIndex, rng);
        }
    }
}
