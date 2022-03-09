using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IControlableObject obj = other.GetComponentInParent<IControlableObject>();
        if(obj != null)
        {
            GameManager.Instance.RealoadCurrentScene();
        }
    }
}
