using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorOpener[] _doorOpener;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private NavMeshObstacle _obstacle;
    [SerializeField] private SkinnedMeshRenderer _graphics;

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
            StartCoroutine(OpenDoorAnimation());

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

    private IEnumerator OpenDoorAnimation()
    {
        while (_graphics.GetBlendShapeWeight(0) <= 100.0f)
        {
            yield return new WaitForSeconds(0.01f);
            _graphics.SetBlendShapeWeight(0, _graphics.GetBlendShapeWeight(0) + 1.0f);
        }

        _obstacle.enabled = false;
        _collider.enabled = false;
    }

}
