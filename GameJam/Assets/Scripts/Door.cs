using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DoorOpener[] _doorOpener;

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
    }

    private void TryOpenDoor()
    {
        if(_doorOpenerNumber == _doorOpener.Length - 1)
        {
            _isOpen = true;
            //animator.SetTrigger("");

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
