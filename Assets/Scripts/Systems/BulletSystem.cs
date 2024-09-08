using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BulletTag>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        var handle = new BulletJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
        }.ScheduleParallel(state.Dependency);

        state.Dependency = handle;
    }
    
    public partial struct BulletJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer.ParallelWriter ecb;

        private void Execute(BulletAspect bulletAspect, [EntityIndexInQuery] int entityIndex, Entity entity)
        {
            bulletAspect.HandleMovement(deltaTime); 
            bulletAspect.HandleLifetime(deltaTime, ref ecb, entityIndex, entity);
        }
    }
}
