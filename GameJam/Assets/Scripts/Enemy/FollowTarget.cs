using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;

    private bool _followTarget = true;

    public void StopFollow()
    {
        _followTarget = false;
        _agent.isStopped = true;
    }

    public void StartFollow()
    {
        _followTarget = true;
    }

    public void SetTarget(Transform target)
    {
        _agent.isStopped = true;
        _target = target;

        StartFollow();
    }

    private void Update()
    {
        if (_target == null || !_followTarget) return;
        _agent.SetDestination(_target.transform.position);
    }
}
