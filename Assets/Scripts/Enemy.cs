using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string AttackParametrName = "isAttack";

    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    private Transform _playerTarget;
    private Animator _animator;

    private int _targetPoint;
    private bool _isRight;
    private bool _isMoveToPlayer;
    private bool _isDestinationCompleted;
    private float _localscaleX;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _targetPoint = 0;
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
        if (collision.TryGetComponent(out Knight knight))
        {
            _playerTarget = knight.transform;
            _isMoveToPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Knight knight))
        {
            _isMoveToPlayer = false;
            Flip(_points[_targetPoint]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Knight knight))
        {
            _isDestinationCompleted = true;
            _animator.SetBool(AttackParametrName, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Knight knight))
        {
            _isDestinationCompleted = false;
            _animator.SetBool(AttackParametrName, false);
        }
    }
}
