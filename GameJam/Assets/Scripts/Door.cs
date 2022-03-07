using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DoorOpener[] _doorOpener;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private NavMeshObstacle _obstacle;

    private int _doorOpenerNumber;
    private bool _isOpen;

    private void Start()
    {
        foreach (DoorOpener opener in _doorOpener)
        {
            opener.DoorOpenerEnableEvent += OpenerActivated;
        }        
    }

    private void OpenerActivated()
    {
        if (_isOpen) return;
        _doorOpenerNumber++;

        TryOpenDoor();
    }

    private void TryOpenDoor()
    {
        if(_doorOpenerNumber == _doorOpener.Length)
        {
            _isOpen = true;
            _obstacle.enabled = false;
            _collider.enabled = false;
            _animator.SetTrigger("OpenDoor");

            foreach (DoorOpener opener in _doorOpener)
            {
                opener.DoorIsOpen();
            }
        }
    }

    private void OpenerDisabled()
    {
        if (_isOpen) return;
        _doorOpenerNumber--;
    }

}
