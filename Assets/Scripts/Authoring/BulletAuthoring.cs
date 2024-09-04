using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletAuthoring : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private class BulletBaker : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new BulletTag());
            AddComponent(entity, new BulletComponent()
            {
                speed = authoring.bulletSpeed,
            });
        }
    }
}
