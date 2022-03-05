using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class LimbModelController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.0f;

    private Rigidbody _rigidBody;
    private float _movementX;
    private float _movementY;

    private bool _isFusionnable = false;
    private string _targetTag = null;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        _rigidBody.AddForce(movement * _speed);
        Rotation();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void OnFusion()
    {
        if (_isFusionnable && _targetTag == "Player")
        {
            // GameManager.Instance.GetPlayer().gameObject.GetComponent<PlayerController>().Split(false);
            FindObjectOfType<ModelController>().ModelReset();
            Destroy(this.gameObject);
        }
    }

    private void Rotation()
    {
        Vector3 direction = _rigidBody.velocity;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.1f)
        {
            _rigidBody.rotation = Quaternion.LookRotation(direction, new Vector3(0, 1, 0));
        }
        else
        {
            _rigidBody.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _targetTag = other.tag;
        _isFusionnable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _targetTag = null;
        _isFusionnable = false;
    }
}
