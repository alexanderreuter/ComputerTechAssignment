using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAuthoring : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    
    private class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            float3 startPos = new float3(Random.Range(-12f, 12f), Random.Range(15f, 20f), 0f);
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new EnemyTagComponent());
            AddComponent(entity, new LifetimeComponent() {lifetime = authoring.lifetime});
            AddComponent(entity, new EnemyComponent()
            {
                speed = authoring.speed,
                startPos = startPos,
                startPosSet = false
            });
        }
    }
}
