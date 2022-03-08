using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class LimbModelController : MonoBehaviour, IControlableObject
{
    [SerializeField] private float _impulseStrength = 0.0f;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;

    [HideInInspector] public EnumLimb EnumLimb;

    private ModelController _modelController;

    private float _movementX;
    private float _movementY;

    private bool _isFusionnable = false;
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
            _rigidBody.velocity = new Vector3(0f, _rigidBody.velocity.y, 0f); ;
            _rigidBody.angularVelocity = new Vector3(0f, _rigidBody.angularVelocity.y, 0f); ;
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
        if (_isFusionnable)
        {
            GameManager.Instance.RemoveControlableObject(this);
            Destroy(this.gameObject);
            _modelController.ModelReset(EnumLimb);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isFusionnable = true;
            _modelController = other.GetComponentInParent<ModelController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
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
        _playerInput.enabled = true;
       // _playerInput.ActivateInput();
    }

    public void DisableController()
    {
        _playerInput.enabled = false;
        //_playerInput.DeactivateInput();
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}
