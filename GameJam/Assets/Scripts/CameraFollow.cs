using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    private Transform _target;

    [SerializeField] private Camera _cam;
    [SerializeField] private Vector3 _offset;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Instance of CameraFollow already exist");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        _target = FindObjectOfType<ModelController>().gameObject.transform;
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        transform.position = _target.position + _offset;
    }

    public void ChangeTarget(Transform target)
    {
        _target = target;
    }
}
