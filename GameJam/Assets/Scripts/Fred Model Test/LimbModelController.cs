using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class LimbModelController : MonoBehaviour, IControlableObject
{
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;

    private float _movementX;
    private float _movementY;

    private bool _isFusionnable = false;
    private string _targetTag = null;

    private bool _canMoveAnimation;

    private void Start()
    {
        GameManager.Instance.AddControlableObject(this);
        GameManager.Instance.ChangeControlableObjectSpecificFocus(this);
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

    private void OnFusion()
    {
        if (_isFusionnable && _targetTag == "Player")
        {
            FindObjectOfType<ModelController>().ModelReset();
            GameManager.Instance.RemoveControlableObject(this);
            Destroy(this.gameObject);
        }
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        _rigidBody.AddForce(movement * _speed);
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

    void OnChangeFocus()
    {
        GameManager.Instance.ChangeControlableObjectFocus();
    }

    public void EnableController()
    {
        _playerInput.ActivateInput();
    }

    public void DisableController()
    {
        _playerInput.DeactivateInput();
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}
