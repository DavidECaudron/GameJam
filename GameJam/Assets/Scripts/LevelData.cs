using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Min(1)] public int LevelNumber;
    [Min(0f)] public float SpawnEnemyTime;
    public Transform EnemySpawnPosition;
    public Transform PlayerSpawnPosition;
    [Min(1f)] public float PlayerStartScaleSize;
}
