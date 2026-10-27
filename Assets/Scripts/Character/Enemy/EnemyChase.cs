using UnityEngine;

[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;
    [SerializeField] private float _followSpeed = 2f;

    private Rigidbody2D _rb;

    private CharacterAnimator _characterAnimator;
    private EnemyVision _enemyVision;
    private float _direction;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _enemyVision = GetComponent<EnemyVision>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_enemyVision.IsPlayerVisible)
        {
            Follow();
        }
    }

    private void Follow()
    {
        if (_playerTarget == null)
            return;

        _direction = Mathf.Sign(_playerTarget.position.x - transform.position.x);
        _rb.linearVelocity = new Vector2(_direction * _followSpeed, _rb.linearVelocity.y);

        _characterAnimator.Walk(_direction != 0);
        _characterAnimator.SetFacing(_direction);
    }
}
