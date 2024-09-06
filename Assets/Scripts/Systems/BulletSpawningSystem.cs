using Unity.Collections;
using Unity.Entities;

public partial struct BulletSpawningSystem : ISystem
{
    private EntityManager entityManager;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTagComponent>();
        entityManager = state.EntityManager;
    }
    
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        
        foreach (var bulletSpawningAspect in SystemAPI.Query<BulletSpawningAspect>())
        {
            bulletSpawningAspect.HandleShooting(SystemAPI.Time.ElapsedTime, ref ecb);
        }
        
        ecb.Playback(entityManager);
        ecb.Dispose();
    }
}

