using UnityEngine;
using UnityEngine.AI;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    private int index = 0;
    private bool _invertPath;

    public Vector3 GetFirstPosition()
    {
        return _path[0].transform.position;
    }

    public Vector3 GetNextPosition(NavMeshAgent agent)
    {
        if (index == _path.Length - 1)
        {
            _invertPath = true;
        }
        else if (_invertPath && index == 0)
        {
            _invertPath = false;
        }

        index = _invertPath ? --index : ++index;

        Vector3 position = _path[index].transform.position;

        if (!VerifyPath(agent,position))
        {
            _invertPath = !_invertPath;
            position = GetNextPosition(agent);
        }

        return position;
    }

    private bool VerifyPath(NavMeshAgent agent, Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(targetPosition, path);

        return path.status != NavMeshPathStatus.PathPartial;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < _path.Length - 1; i++)
        {
            Gizmos.DrawLine(_path[i].transform.position, _path[i + 1].transform.position);
        }
    }
}
