using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy Set", menuName = "Enemy Set")]
public class EnemySet : ScriptableObject {
    [SerializeField]
    public List<EnemyMap> enemies;
}
[System.Serializable]
public class EnemyMap
{
    public EnemyType enemyType;
    public GridPosition position;
}
