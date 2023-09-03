using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rightMoveRadius;
    [SerializeField] private float _leftMoveRadius;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isLeftMove, _isRightMove;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var changeDirectionCoroutine = StartCoroutine(RandomChangeDirection());
    }

    private void Update()
    {
        if (_isLeftMove)
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);

            if (transform.position.x < _leftMoveRadius)
            {
                _isLeftMove = false;
                _animator.SetBool("isWalk", false);
            }
        }
        else if (_isRightMove)
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);

            if (transform.position.x > _rightMoveRadius)
            {
                _isRightMove = false;
                _animator.SetBool("isWalk", false);
            }
        }
    }

    private IEnumerator RandomChangeDirection()
    {
        var waitSecond = new WaitForSeconds(1f);
        int random;
        int minRandomValue = 0;
        int maxRadomValue = 3;

        while (true)
        {
            random = Random.Range(minRandomValue, maxRadomValue);

            if (random == minRandomValue)
            {
                _isLeftMove = false;
                _isRightMove = true;

                _animator.SetBool("isWalk", true);
                _spriteRenderer.flipX= true;
            }
            else if (random == maxRadomValue - 1)
            {
                _isRightMove = false;
                _isLeftMove = true;

                _animator.SetBool("isWalk", true);
                _spriteRenderer.flipX = false;
            }
            else
            {
                _isRightMove= false;
                _isLeftMove = false;

                _animator.SetBool("isWalk", false);
            }

            yield return waitSecond;
        }
    }
}
