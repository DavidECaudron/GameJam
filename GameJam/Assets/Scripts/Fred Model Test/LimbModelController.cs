using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class LimbModelController : MonoBehaviour
{
    [SerializeField] private ModelController _modelController;
    [SerializeField] private float _impulseStrength = 0.0f;

    private Rigidbody _rigidBody;
    private float _movementX;
    private float _movementY;

    private bool _isFusionnable = false;

    private Animator _animator;
    private bool _canMoveAnimation;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
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

    private void OnFusion()
    {
        if (_isFusionnable)
        {
            // GameManager.Instance.GetPlayer().gameObject.GetComponent<PlayerController>().Split(false);
            _modelController.ModelReset();
            Destroy(this.gameObject);
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
}
