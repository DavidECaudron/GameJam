using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyPath _path;
    [SerializeField] private float _distanceForNextTarget = .5f;
    [SerializeField] private float _chasingDistance = 10f;

    private Vector3 _pathTargetPosition;
    private bool _chasing;
    private Vector3 _positionBeforeChasing;
    private bool _backInLastPosition;

    private void Start()
    {
        if(_path == null)
        {
            _path = FindObjectOfType<EnemyPath>();
            if(_path == null)
            {
                Debug.LogError("Not path found");
                gameObject.SetActive(false);
                return;
            }
        }

        if (!_agent.isOnNavMesh)
        {
            Debug.LogError("No navmesh set, need to bake terrain");
            gameObject.SetActive(false);
            return;
        }

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
        if (_backInLastPosition)
        {
            _agent.SetDestination(_positionBeforeChasing);

            if (CheckDistance(_positionBeforeChasing, _distanceForNextTarget))
            {
                _backInLastPosition = false;
                _agent.SetDestination(_pathTargetPosition);
            }

            return;
        }

        if (CheckDistance(_pathTargetPosition, _distanceForNextTarget))
        {
            _pathTargetPosition = _path.GetNextPosition(_agent);
            _agent.SetDestination(_pathTargetPosition);
        }
    }

    private void ChaseTarget()
    {
        if (CheckDistance(_target.transform.position, _chasingDistance))
        {
            _agent.SetDestination(_target.transform.position);
        }
        else
        {
            StopChasing();
        }
    }

    public void StopChasing()
    {
        _chasing = false;
        _target = null;
        _backInLastPosition = true;
    }

    public void StartChasing(Transform target)
    {
        _target = target;
        _positionBeforeChasing = transform.position;
        _chasing = true;
    }

    private bool CheckDistance(Vector3 targetPosition, float maxDistance)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance <= maxDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chasingDistance);
    }
}
