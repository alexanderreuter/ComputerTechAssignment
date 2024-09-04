using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public readonly partial struct EnemySpawninngAspect : IAspect
{
    public readonly RefRO<PrefabComponent> enemyPrefab;
    

}
