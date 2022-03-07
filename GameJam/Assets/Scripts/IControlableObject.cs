
using UnityEngine;

public interface IControlableObject
{
    void EnableController();
    void DisableController();
    Transform GetTransform();
}
