using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private float _size;
    public float Size
    {
        get { return _size; }
        private set { _size = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
    }
}
