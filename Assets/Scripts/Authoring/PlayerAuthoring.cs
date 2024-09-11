using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAuthoring : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float3 bulletSpawnPos;
    
    private class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerTagComponent());
            AddComponent(entity, new PlayerComponent() {speed = authoring.speed});
            AddComponent(entity, new InputComponent());
            AddComponent(entity, new FloatComponent() {value = 0f});
            AddComponent(entity, new BulletSpawnDataComponent()
            {
                prefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                bulletSpawnPos = authoring.bulletSpawnPos,
                shootCooldown = authoring.shootCooldown
            });
        }
    }
}
