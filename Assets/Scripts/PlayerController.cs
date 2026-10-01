using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharracterAnimator))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private CharracterAnimator _characterAnimator;
    private Rigidbody2D _rigidbody;

    private bool _isLeftMoving, _isRightMoving, _isCanJump;
    private float _maxMapRadius = 8f;
    private float _minMapRadius = -8f;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharracterAnimator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isRightMoving && transform.position.x < _maxMapRadius)
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (_isLeftMoving && transform.position.x > _minMapRadius)
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
        }
    }

    public void MoveRight(bool isActive)
    {
        _characterAnimator.Walk(isActive);
        _isRightMoving = isActive;
        Flip(1);
    }


    public void MoveLeft(bool isActive)
    {
        _characterAnimator.Walk(isActive);
        _isLeftMoving = isActive;
        Flip(-1);
    }

    private void Flip(float localScaleX)
    {
        Vector2 shouldLook = transform.localScale;

        shouldLook.x = localScaleX;
        transform.localScale = shouldLook;
    }

    public void Jump()
    {
        if (_isCanJump)
        {
            _characterAnimator.Jump(true);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isCanJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            _isCanJump = true;
            _characterAnimator.Jump(false);
        }
    }
}
