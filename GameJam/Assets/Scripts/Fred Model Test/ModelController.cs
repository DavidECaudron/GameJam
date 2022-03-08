using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ModelController : MonoBehaviour, IControlableObject
{
    [SerializeField] private float _speed = 0.0f;

    [SerializeField] private SkinnedMeshRenderer _leftEye;
    [SerializeField] private SkinnedMeshRenderer _rightEye;
    [SerializeField] private SkinnedMeshRenderer _leftArm;
    [SerializeField] private SkinnedMeshRenderer _rightArm;

    [SerializeField] private GameObject _eye;
    [SerializeField] private GameObject _arm;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] _walkSounds;
    [SerializeField] private AudioClip _eatSound;
    [SerializeField] private AudioClip _deathSound;

    private float _movementX;
    private float _movementY;
    private bool _canMoveAnimation;
    public bool Alive;
    public bool CanMove;

    private void FixedUpdate()
    {
        if (!CanMove) return;

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
        DisableController();

        if (inputVector.x > 0.0f && _leftEye.enabled == true)
        {
            _leftEye.enabled = false;
            _eye.GetComponent<LimbModelController>().EnumLimb = EnumLimb.LeftEye;
            Instantiate(_eye, new Vector3(_leftEye.transform.position.x, _leftEye.transform.position.y + 3.0f, _leftEye.transform.position.z), Quaternion.identity);
        }
        if (inputVector.x < 0.0f && _rightEye.enabled == true)
        {
            _rightEye.enabled = false;
            _eye.GetComponent<LimbModelController>().EnumLimb = EnumLimb.RightEye;
            Instantiate(_eye, new Vector3(_rightEye.transform.position.x, _rightEye.transform.position.y + 3.0f, _rightEye.transform.position.z), Quaternion.identity);
        }

        if (inputVector.y > 0.0f && _leftArm.enabled == true)
        {
            _leftArm.enabled = false;
            _arm.GetComponent<LimbModelController>().EnumLimb = EnumLimb.LeftArm;
            Instantiate(_arm, new Vector3(_leftArm.transform.position.x, _leftArm.transform.position.y + 3.0f, _leftArm.transform.position.z + 0.75f), Quaternion.identity);
        }
        if (inputVector.y < 0.0f && _rightArm.enabled == true)
        {
            _rightArm.enabled = false;
            _arm.GetComponent<LimbModelController>().EnumLimb = EnumLimb.RightArm;
            Instantiate(_arm, new Vector3(_rightArm.transform.position.x, _rightArm.transform.position.y + 3.0f, _rightArm.transform.position.z + 0.75f), Quaternion.identity);
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

    public void ModelReset(EnumLimb enumLimb)
    {

        if (enumLimb == EnumLimb.LeftEye)
        {
            _leftEye.enabled = true;
        }
        if (enumLimb == EnumLimb.RightEye)
        {
            _rightEye.enabled = true;
        }
        if (enumLimb == EnumLimb.LeftArm)
        {
            _leftArm.enabled = true;
        }
        if (enumLimb == EnumLimb.RightArm)
        {
            _rightArm.enabled = true;
        }

        EnableController();
        GameManager.Instance.ChangeControlableObjectSpecificFocus(this);
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

    private void PlayWalkSound()
    {
        int index = Random.Range(0, _walkSounds.Length);
        AudioManager.Instance.PlaySoundAt(_walkSounds[index], transform);
    }

    private void PlayEatSound()
    {
        AudioManager.Instance.PlaySoundAt(_eatSound, transform);
    }

    private void PlayDeathSound()
    {
        AudioManager.Instance.PlaySoundAt(_deathSound, transform);
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

    public void Die()
    {
        Debug.Log("Die");
        Alive = false;
        //PlayDeathSound();
        //Jouer animation mort
    }
}
