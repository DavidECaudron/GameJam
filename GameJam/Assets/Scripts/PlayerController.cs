using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.0f;

    private Rigidbody _rigidBody;
    private float _movementX;
    private float _movementY;

    PlayerForm[] playeforms;

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
        if (other.tag == "EatableObject")
        {
            EatableObject eatable = other.GetComponent<EatableObject>();
            if (eatable == null) return;


            if (eatable.ChangePlayerForm)
            {
                //changer forme du joueur

                PlayerForm form = playeforms.FirstOrDefault(x => x.PlayerFormEnum == eatable.PlayerFormEnum);

            }

            Destroy(eatable.gameObject);
            Debug.Log("Bitoniau");
        }


    }
}
