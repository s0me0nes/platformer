using UnityEngine;

[RequireComponent(typeof(CharracterAnimator))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    private CharracterAnimator _charracterAnimator;
    private Transform _playerTarget;

    private int _targetPoint = 0;
    private bool _isRight;
    private bool _isMoveToPlayer;
    private bool _isDestinationCompleted;
    private float _localscaleX;

    private void Awake()
    {
        _charracterAnimator = GetComponent<CharracterAnimator>();
    }

    private void Update()
    {
        if (!_isDestinationCompleted)
        {
            if (_isMoveToPlayer)
            {
                MoveDirection(_playerTarget);
                return;
            }

            MoveDirection(_points[_targetPoint]);

            if (transform.position.x == _points[_targetPoint].position.x)
            {
                ChangeTargetPoint();
            }
        }
    }

    private void MoveDirection(Transform target)
    {
        transform.position =
        Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    private void ChangeTargetPoint()
    {
        if (_targetPoint >= _points.Length - 1)
        {
            _targetPoint = 0;
        }
        else
        {
            _targetPoint++;
        }

        Flip(_points[_targetPoint]);
    }

    private void Flip(Transform lookTarget)
    {
        if (lookTarget == null) return;

        _isRight = transform.position.x < lookTarget.position.x;
        Vector2 shouldLook = transform.localScale;

        if (_isRight)
        {
            _localscaleX = 1;
            shouldLook.x = _localscaleX;
        }
        else
        {
            _localscaleX = -1;
            shouldLook.x = _localscaleX;
        }

        transform.localScale = shouldLook;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController knight))
        {
            _playerTarget = knight.transform;
            _isMoveToPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController knight))
        {
            _isMoveToPlayer = false;
            Flip(_points[_targetPoint]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController knight))
        {
            _isDestinationCompleted = true;
            _charracterAnimator.Attack(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController knight))
        {
            _isDestinationCompleted = false;
            _charracterAnimator.Attack(false);
        }
    }
}
