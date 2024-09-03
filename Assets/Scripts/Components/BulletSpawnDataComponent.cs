using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct BulletSpawnDataComponent : IComponentData
{
    public Entity prefab;
    public float speed;
    public float shootCooldown;
}
