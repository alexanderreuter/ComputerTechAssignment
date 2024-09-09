using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    
    private class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new EnemySpawnerTagComponent());
            AddComponent(entity, new FloatComponent() {value = 0});
            AddComponent(entity, new EnemySpawnDataComponent()
            {
                prefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                spawnInterval = authoring.spawnInterval
            });
        }
    }
}
