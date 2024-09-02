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
    
    private class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            // AddComponent(entity, new PositionComponent { position = authoring.initialPosition} );
            AddComponent(entity, new PlayerTagComponent());
            AddComponent(entity, new MovementComponent { speed = authoring.speed } );
            AddComponent(entity, new InputComponent());
        }
    }
}
