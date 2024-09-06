using System.Collections;
using System.Collections.Generic;using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

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
        foreach (var enemyPrefab in SystemAPI.Query<RefRO<PrefabComponent>>())
        {
            // Entity enemyEntity = entityManager.Instantiate(enemyPrefab.ValueRO.prefab);
            //
            // LocalTransform enemyTransform = entityManager.GetComponentData<LocalTransform>(enemyEntity);
            // enemyTransform.Position = new float3(Random.Range(-12f, 12f), 5.25f, 0f);
            // entityManager.SetComponentData(enemyEntity, enemyTransform);
        }
    }
}
