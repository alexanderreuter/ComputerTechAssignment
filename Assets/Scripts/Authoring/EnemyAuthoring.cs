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
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new EnemyTagComponent());
            AddComponent(entity, new LifetimeComponent() {lifetime = authoring.lifetime});
            AddComponent(entity, new EnemyComponent() {speed = authoring.speed});
        }
    }
}
