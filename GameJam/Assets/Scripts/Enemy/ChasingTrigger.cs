using UnityEngine;

public class ChasingTrigger : MonoBehaviour
{
    [SerializeField] private FollowTarget _followTarget;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;
        if (_followTarget.OnChasing()) return;

        ModelController model = other.GetComponent<ModelController>();
        if (!model.Alive) return;

        _followTarget.StartChasing(other.transform);
    }
}
