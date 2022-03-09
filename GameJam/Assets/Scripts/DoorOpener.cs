using UnityEngine;

public enum DoorOpenerType
{
    Crank = 0,
    Slab = 1
}

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DoorOpenerType OpenerType;
    [SerializeField] private bool _needMinimumSizeForUse;
    [SerializeField] private float _minSize;

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

        //_animator.SetTrigger("DownSlab");

        DoorOpenerEnableEvent?.Invoke();
    }

    private void DisableOpener()
    {
        if (_doorIsOpen) return;

        //_animator.SetTrigger("UpSlab");

        DoorOpenerDisableEvent?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_doorIsOpen || _uniqueUtilisation) return;
        if (other.tag == "Player")
        {
            if (_needMinimumSizeForUse)
            {
                if (other.transform.localScale.y < _minSize) return;
            }

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
