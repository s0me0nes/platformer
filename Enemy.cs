using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    private SpriteRenderer _spriteRenderer;
    private int _targetPoint;
    private bool _isRight;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _targetPoint = 0;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _points[_targetPoint].position, _speed * Time.deltaTime);

        if (transform.position.x == _points[_targetPoint].position.x)
        {
            ChangeTargetPoint();
        }
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

        _isRight = transform.position.x < _points[_targetPoint].position.x;
        _spriteRenderer.flipX = _isRight;
    }
}
