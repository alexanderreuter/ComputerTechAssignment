using Unity.Entities;
using Unity.Mathematics;

public struct EnemyComponent : IComponentData
{
    public float speed;
    public float3 startPos;
    public bool startPosSet;
}
