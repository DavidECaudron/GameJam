using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class FollowCam : MonoBehaviour
{
    public static FollowCam Instance;

    [SerializeField] private CinemachineVirtualCamera _cam;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Instance of FollowCam already exist");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void ChangeTarget(Transform target)
    {
        _cam.Follow = target;
    }
}
