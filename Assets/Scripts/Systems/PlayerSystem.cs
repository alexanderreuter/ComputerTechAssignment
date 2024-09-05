using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public partial struct PlayerSystem : ISystem
{
    // private EntityManager entityManager;
    
    public void OnCreate(ref SystemState state)
    {
        // entityManager = state.EntityManager;
        state.RequireForUpdate<PlayerTagComponent>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        new MoveJob
        {
            ecb = new EntityCommandBuffer(Allocator.TempJob),
            deltaTime = SystemAPI.Time.DeltaTime,
            elapsedTime = SystemAPI.Time.ElapsedTime,

        }.Schedule();
        
        // foreach (var playerAspect in SystemAPI.Query<PlayerAspect>())
        // {
        //     playerAspect.HandleMovement(SystemAPI.Time.DeltaTime);
        //     playerAspect.HandleShooting(SystemAPI.Time.ElapsedTime, entityManager);
        // }
    }
    
    public partial struct MoveJob : IJobEntity
    {
        public float deltaTime;
        public double elapsedTime;
        public EntityCommandBuffer ecb;

        private void Execute (ref LocalTransform transform, ref FloatComponent nextShootTime, in MovementComponent movement, 
            in InputComponent input, in BulletSpawnDataComponent bulletSpawnData)
        {
            //Movement
            float3 position = transform.Position;
            position.x = input.moveInput.x;
            position.y = input.moveInput.y;

            transform.Position += position * movement.speed * deltaTime;
            
            // Shooting
            if (input.isShooting && nextShootTime.value < elapsedTime)
            {
                Entity bulletEntity = ecb.Instantiate(bulletSpawnData.prefab);
                
                ecb.AddComponent<LocalTransform>(bulletEntity, new LocalTransform()
                {
                    Rotation = transform.Rotation,
                    Position = transform.Position + bulletSpawnData.bulletSpawnPos
                });
               
                // ecb.SetComponent<LocalTransform>(bulletEntity);
                // LocalTransform bulletTransform = ecb.GetComponent<LocalTransform>(bulletEntity);
                // bulletTransform.Rotation = transform.Rotation;
                // bulletTransform.Position =  transform.Position + bulletSpawnData.bulletSpawnPos;
                // ecb.SetComponentData<LocalTransform>(bulletEntity, bulletTransform);
            
                nextShootTime.value = (float)elapsedTime + bulletSpawnData.shootCooldown;
            }
        }
    }
}
