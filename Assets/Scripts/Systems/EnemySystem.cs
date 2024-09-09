using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial struct EnemySystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemyTagComponent>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        var handle = new EnemyJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
        }.ScheduleParallel(state.Dependency);

        state.Dependency = handle;
    }
    
    public partial struct EnemyJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer.ParallelWriter ecb;

        private void Execute(EnemyAspect enemyAspect, [EntityIndexInQuery] int entityIndex, Entity entity)
        {
            enemyAspect.HandleMovement(deltaTime);
            enemyAspect.HandleLifetime(deltaTime, ref ecb, entityIndex, entity);
        }
    }
}
