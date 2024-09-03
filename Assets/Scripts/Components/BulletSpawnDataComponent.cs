using Unity.Entities;
using Unity.Mathematics;

public struct BulletSpawnDataComponent : IComponentData
{
    public Entity prefab;
    public float3 bulletSpawnPos;
    public float shootCooldown;
}
