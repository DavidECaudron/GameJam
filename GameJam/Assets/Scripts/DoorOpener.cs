using System.Collections;
using UnityEngine;

public enum DoorOpenerType
{
    Crank = 0,
    Slab = 1
}

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private DoorOpenerType OpenerType;
    [SerializeField] private bool _needMinimumSizeForUse;
    [SerializeField] private float _minSize;
    [SerializeField] private SkinnedMeshRenderer _graphics;

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

        StartCoroutine(SlabAnim());

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
        ModelController model = other.GetComponentInParent<ModelController>();
        if (model != null)
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
        ModelController model = other.GetComponentInParent<ModelController>();
        if (model != null)
        {
            DisableOpener();
        }
    }

    public void DoorIsOpen()
    {
        _doorIsOpen = true;
    }

    private IEnumerator SlabAnim()
    {
        while (_graphics.GetBlendShapeWeight(0) <= 100.0f)
        {
            yield return new WaitForSeconds(0.01f);
            _graphics.SetBlendShapeWeight(0, _graphics.GetBlendShapeWeight(0) + 1.0f);
        }
    }
}
