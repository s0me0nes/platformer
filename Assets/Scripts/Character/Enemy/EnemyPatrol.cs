using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(EnemyVision))]
public class EnemyPatrol : MonoBehaviour
{
    public float Direction { get; private set; }

    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _reachDistance = 0.1f;

    private CharacterAnimator _characterAnimator;
    private EnemyVision _enemyVision;
    private Rigidbody2D _rb;

    private int _currentIndex = 0;

    private void Awake()
    {
        Direction = 1;
        _enemyVision = GetComponent<EnemyVision>();
        _rb = GetComponent<Rigidbody2D>();
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void FixedUpdate()
    {
        if (!_enemyVision.IsPlayerVisible)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (_points[_currentIndex] == null)
        {
            return;
        }

        Transform target = _points[_currentIndex];
        Direction = Mathf.Sign(target.position.x - transform.position.x);

        _rb.linearVelocity = new Vector2(Direction * _speed, _rb.linearVelocity.y);

        if (Mathf.Abs(target.position.x - transform.position.x) < _reachDistance)
        {
            _currentIndex++;

            if (_currentIndex >= _points.Length)
                _currentIndex = 0;
        }

        _characterAnimator.Walk(Direction != 0);
        _characterAnimator.SetFacing(Direction);
    }
}