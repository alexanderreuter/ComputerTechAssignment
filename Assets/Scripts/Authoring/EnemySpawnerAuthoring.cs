using Unity.Entities;
using UnityEngine;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    private class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new EnemySpawnerTagComponent());
            AddComponent(entity, new PrefabComponent
            {
                prefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}
