using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _eye;
    [SerializeField] private GameObject _arm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Buff")
        {
            Buff buff = other.GetComponentInParent<Buff>();
            _parent.transform.localScale = new Vector3(_parent.transform.localScale.x + buff.Size, _parent.transform.localScale.y + buff.Size, _parent.transform.localScale.z + buff.Size);
            _eye.transform.localScale = new Vector3(_eye.transform.localScale.x + buff.Size, _eye.transform.localScale.y + buff.Size, _eye.transform.localScale.z + buff.Size);
            _arm.transform.localScale = new Vector3(_arm.transform.localScale.x + buff.Size, _arm.transform.localScale.y + buff.Size, _arm.transform.localScale.z + buff.Size);
            Destroy(buff.gameObject);
        }
    }
}
