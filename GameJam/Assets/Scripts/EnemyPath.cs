using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] _path;
    private int index = 0;
    private bool _invertPath;

    public Vector3 GetFirstPosition()
    {
        return _path[0].transform.position;
    }

    public Vector3 GetNextPosition()
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

        return _path[index].transform.position;
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
