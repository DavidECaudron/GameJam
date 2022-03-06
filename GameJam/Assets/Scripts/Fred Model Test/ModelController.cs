using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ModelController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.0f;

    [SerializeField] private SkinnedMeshRenderer _leftEye;
    [SerializeField] private SkinnedMeshRenderer _rightEye;
    [SerializeField] private SkinnedMeshRenderer _leftArm;
    [SerializeField] private SkinnedMeshRenderer _rightArm;

    [SerializeField] private GameObject _eye;
    [SerializeField] private GameObject _arm;

    private Rigidbody _rigidBody;
    private float _movementX;
    private float _movementY;

    private PlayerInput _playerInput;

    private Animator _animator;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        bool condition = (_movementX != 0.0f || _movementY != 0.0f);
        _animator.SetBool("Move", condition);

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

    /*
    private void OnSplit()
    {
        _leftEye.enabled = false;
        _rightEye.enabled = false;
        _leftArm.enabled = false;
        _rightArm.enabled = false;
    }
    */
    /*
    private void OnFusion()
    {
        _leftEye.enabled = true;
        _rightEye.enabled = true;
        _leftArm.enabled = true;
        _rightArm.enabled = true;
    }
    */

    private void OnSplitModel(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        if (inputVector.x > 0.0f)
        {
            _leftEye.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_eye, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z + 2.0f), Quaternion.identity);
        }
        if (inputVector.x < 0.0f)
        {
            _rightEye.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_eye, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z + 2.0f), Quaternion.identity);
        }
        if (inputVector.y > 0.0f)
        {
            _leftArm.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_arm, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z + 2.0f), Quaternion.identity);
        }
        if (inputVector.y < 0.0f)
        {
            _rightArm.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_arm, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z + 2.0f), Quaternion.identity);
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

    public void ModelReset()
    {
        _leftEye.enabled = true;
        _rightEye.enabled = true;
        _leftArm.enabled = true;
        _rightArm.enabled = true;
        _playerInput.enabled = true;
    }
}
