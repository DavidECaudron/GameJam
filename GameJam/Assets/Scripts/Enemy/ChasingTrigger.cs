using UnityEngine;

public class ChasingTrigger : MonoBehaviour
{
    [SerializeField] private FollowTarget _followTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        _followTarget.StartChasing(other.transform);
    }
}
