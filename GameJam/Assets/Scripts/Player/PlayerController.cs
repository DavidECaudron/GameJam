using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _eye;
    [SerializeField] private MeshRenderer _eyeMesh;
    [SerializeField] private SphereCollider _eyeCollider;

    [SerializeField] private float _speed = 0.0f;

    private Rigidbody _rigidBody;
    private float _movementX;
    private float _movementY;

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

    private void OnSplit()
    {
        Instantiate(_eye, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1.0f), Quaternion.identity);
        Split(true);
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

    public void Split(bool test)
    {
        this.GetComponent<PlayerInput>().enabled = !test;
        _eyeMesh.enabled = !test;
        _eyeCollider.enabled = !test;
    }
}
