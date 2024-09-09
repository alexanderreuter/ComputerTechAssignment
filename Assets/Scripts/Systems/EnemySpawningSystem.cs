using Unity.Entities;
using RandomUnity = UnityEngine.Random;
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
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            seed = RandomUnity.Range(1, 1000)
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
        public int seed;

        private void Execute(EnemySpawningAspect enemySpawningAspect, [EntityIndexInQuery] int entityIndex)
        {
            Unity.Mathematics.Random rng = new Unity.Mathematics.Random((uint)seed);

            float test = rng.NextFloat(0f, 1f);
            
            enemySpawningAspect.HandleSpawning(deltaTime, ref ecb, entityIndex, rng);
        }
    }
}
