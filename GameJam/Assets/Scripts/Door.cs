using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DoorOpener _doorOpener;

    private void Start()
    {
        _doorOpener.OpenDoorEvent += Open;
    }

    private void Open()
    {
        //_animator.SetTrigger("OpenDoor");
    }

}
