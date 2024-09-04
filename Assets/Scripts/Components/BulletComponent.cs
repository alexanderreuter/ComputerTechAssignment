using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BulletComponent : IComponentData
{
    public float speed;
    public float3 initialPosition;
    public float lifeTime;
}
