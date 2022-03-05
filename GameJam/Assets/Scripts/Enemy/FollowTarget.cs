using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyPath _path;
    [SerializeField] private float _distanceForNextTarget = .5f;

    private Vector3 _pathTargetPosition;
    private bool _chasing;

    private void Start()
    {
        _pathTargetPosition = _path.GetFirstPosition();
        _agent.SetDestination(_pathTargetPosition);
    }

    private void Update()
    {
        if (_chasing)
        {
            ChaseTarget();
        }
        else
        {
            FollowPath();
        }
    }

    private void FollowPath()
    {
        if (GetNextPathPosition())
        {
            _pathTargetPosition = _path.GetNextPosition();
            _agent.SetDestination(_pathTargetPosition);
        }        
    }

    private void ChaseTarget()
    {
        //TODO
    }


    private bool GetNextPathPosition()
    {
        float distance = Vector3.Distance(transform.position, _pathTargetPosition);
        return distance <= _distanceForNextTarget;
    }
}
