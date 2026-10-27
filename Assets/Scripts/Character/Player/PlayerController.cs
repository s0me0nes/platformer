using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Ground))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(ItemCollector))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0,10)] private float _speed = 2.5f;
    [SerializeField, Range(0,10)] private float _jumpForce = 7f;
    
    private float _moveInput = 0;
    private Rigidbody2D _rb;

    private CharacterAnimator _characterAnimator;
    private Ground _ground;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _rb = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _moveInput * _speed;
    }

    private void Update()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        _moveInput = 0;

        if (Input.GetKey(KeyCode.A))
        {
            _moveInput = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveInput = 1;
        }

        _characterAnimator.Walk(_moveInput != 0);
        _characterAnimator.SetFacing(_moveInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }


    private void Jump()
    {
        if (!_ground.IsGrounded())
            return;

        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _characterAnimator.Jump();
    }
}