using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ModelController : MonoBehaviour
{
    [SerializeField] private float _impulseStrength = 0.0f;

    [SerializeField] private SkinnedMeshRenderer _leftEye;
    [SerializeField] private SkinnedMeshRenderer _rightEye;
    [SerializeField] private SkinnedMeshRenderer _leftArm;
    [SerializeField] private SkinnedMeshRenderer _rightArm;

    [SerializeField] private GameObject _eye;
    [SerializeField] private GameObject _arm;

    [SerializeField] private Buff _buff;

    private Rigidbody _rigidBody;
    private float _movementX;
    private float _movementY;
    private bool _canMoveAnimation;

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

        if (!_canMoveAnimation)
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
            return;
        }
        Movement();
        Rotation();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }


    private void OnSplitModel(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        if (inputVector.x > 0.0f)
        {
            _leftEye.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_eye, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 2.0f), Quaternion.identity);
        }
        if (inputVector.x < 0.0f)
        {
            _rightEye.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_eye, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 2.0f), Quaternion.identity);
        }
        if (inputVector.y > 0.0f)
        {
            _leftArm.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_arm, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 2.0f), Quaternion.identity);
        }
        if (inputVector.y < 0.0f)
        {
            _rightArm.enabled = false;
            _playerInput.enabled = false;
            Instantiate(_arm, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 2.0f), Quaternion.identity);
        }
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        _rigidBody.AddForce(movement * _impulseStrength);
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

    #region Animation
    private void CanMoveAnimation()
    {
        _canMoveAnimation = true;
    }

    private void CantMoveAnimation()
    {
        _canMoveAnimation = false;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.transform.position);
    }
}
