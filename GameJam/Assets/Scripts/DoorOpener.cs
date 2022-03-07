using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorOpener : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _uniqueUtilisation;

    private bool _doorIsOpen;

    public delegate void DoorOpenerActivated();
    public event DoorOpenerActivated DoorOpenerEnableEvent;
    public event DoorOpenerActivated DoorOpenerDisableEvent;

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
        if (_doorIsOpen) return;
        if (_uniqueUtilisation) return;
        if (other.tag == "Player")
        {
            EnableOpener();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_doorIsOpen) return;
        if (_uniqueUtilisation) return;
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
