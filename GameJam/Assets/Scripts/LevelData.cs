using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Min(1)] public int LevelNumber;
    [Min(0f)] public float SpawnEnemyTime;
    public Transform EnemySpawnPosition;
}