using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChangeLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelData _levelData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ModelController controller = other.GetComponent<ModelController>();
            if (controller != null)
            {
                controller.CanMove = false;
                GameManager.Instance.LoadScene(_levelData.NextLevelIndex);
            }

        }
    }
}
