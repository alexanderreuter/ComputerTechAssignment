using Unity.Entities;

public struct EnemySpawnDataComponent : IComponentData
{
    public Entity prefab;
    public float spawnInterval;
}
