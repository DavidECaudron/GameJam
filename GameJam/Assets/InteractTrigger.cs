using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Buff")
        {
            Buff buff = other.GetComponentInParent<Buff>();
            this.transform.localScale = new Vector3(_parent.transform.localScale.x + buff.Size, _parent.transform.localScale.y + buff.Size, _parent.transform.localScale.z + buff.Size);
            Destroy(buff.gameObject);
        }
    }
}
