using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public delegate void DoorOpenerActivated();
    public event DoorOpenerActivated OpenDoorEvent;

    private void OpenDoor()
    {
        OpenDoorEvent?.Invoke();
    }
}
