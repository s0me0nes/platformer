using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Knight : MonoBehaviour
{
    private const string WalkParametrName = "isWalk";
    private const string JumpParametrName = "isJump";

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private Text ScoreText;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private int _score = 0;
    private int _needScoreToWin = 5;
    private bool _isLeftMoving, _isRightMoving, _isCanJump;
    private float _maxMapRadius = 8f;
    private float _minMapRadius = -8f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

        ShowScore();
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
        _isRightMoving = isActive;
        _animator.SetBool(WalkParametrName, isActive);
        _spriteRenderer.flipX = false;
    }

    public void MoveLeft(bool isActive)
    {
        _isLeftMoving = isActive;
        _animator.SetBool(WalkParametrName, isActive);
        _spriteRenderer.flipX = true;
    }

    public void Jump()
    {
        if (_isCanJump)
        {
            _animator.SetBool(JumpParametrName, true);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isCanJump = false;
        }
    }

    private void ShowScore()
    {
        ScoreText.text = _score + "/" + _needScoreToWin;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            _isCanJump = true;
            _animator.SetBool(JumpParametrName, false);
        }
        else if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            _score++;
            ShowScore();

            Destroy(collision.gameObject);

            if (_score == _needScoreToWin)
            {
                Debug.Log("You Win!");
            }
        }
        else if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.Log("You Lose!");
        }
    }
}
