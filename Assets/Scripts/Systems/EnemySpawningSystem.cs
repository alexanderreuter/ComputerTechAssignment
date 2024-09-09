using Unity.Entities;
using Random = Unity.Mathematics.Random;

public partial struct EnemySpawningSystem : ISystem
{
    private EntityManager entityManager;
    
    void OnCreate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        state.RequireForUpdate<EnemySpawnerTagComponent>();
    }

    void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        var handle = new EnemySpawnJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
        }.ScheduleParallel(state.Dependency);

        state.Dependency = handle;
        
        //
        // foreach (var enemyPrefab in SystemAPI.Query<RefRO<PrefabComponent>>())
        // {
        //     // Entity enemyEntity = entityManager.Instantiate(enemyPrefab.ValueRO.prefab);
        //     //
        //     // LocalTransform enemyTransform = entityManager.GetComponentData<LocalTransform>(enemyEntity);
        //     // enemyTransform.Position = new float3(Random.Range(-12f, 12f), 5.25f, 0f);
        //     // entityManager.SetComponentData(enemyEntity, enemyTransform);
        // }
    }
    
    public partial struct EnemySpawnJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer.ParallelWriter ecb;

        private void Execute(EnemySpawningAspect enemySpawningAspect, [EntityIndexInQuery] int entityIndex)
        {
            uint seed = 10;
            
            if (entityIndex > 0)
            {
                seed = (uint)entityIndex;
            }
            
            Unity.Mathematics.Random rng = new Unity.Mathematics.Random(seed);
            
            enemySpawningAspect.HandleSpawning(deltaTime, ref ecb, entityIndex, rng);
        }
    }
}
