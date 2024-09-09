using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletAuthoring : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    
    private class BulletBaker : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new BulletTagComponent());
            AddComponent(entity, new BulletComponent()
            {
                speed = authoring.speed,
                lifeTime = authoring.lifetime
            });
        }
    }
}
