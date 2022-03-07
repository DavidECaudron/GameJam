using UnityEngine;

public enum DoorOpenerType
{
    Crank = 0,
    Slab = 1
}

[RequireComponent(typeof(Animator))]
public class DoorOpener : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DoorOpenerType OpenerType;
    private bool _uniqueUtilisation;
    private bool _doorIsOpen;

    public delegate void DoorOpenerActivated();
    public event DoorOpenerActivated DoorOpenerEnableEvent;
    public event DoorOpenerActivated DoorOpenerDisableEvent;

    private void Start()
    {
        _uniqueUtilisation = OpenerType == DoorOpenerType.Crank;
    }

    private void EnableOpener()
    {
        if (_doorIsOpen) return;

        //_animator.SetTrigger("");

        DoorOpenerEnableEvent?.Invoke();
    }

    private void DisableOpener()
    {
        if (_doorIsOpen) return;
        DoorOpenerDisableEvent?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_doorIsOpen || _uniqueUtilisation) return;
        if (other.tag == "Player")
        {
            EnableOpener();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_doorIsOpen || _uniqueUtilisation) return;
        if (other.tag == "Player")
        {
            DisableOpener();
        }
    }

    public void DoorIsOpen()
    {
        _doorIsOpen = true;
    }
}
