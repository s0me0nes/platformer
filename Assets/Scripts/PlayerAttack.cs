using UnityEngine;

[RequireComponent(typeof(CharracterAnimator))]
public class PlayerAttack : MonoBehaviour
{
    private CharracterAnimator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharracterAnimator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemy))
        {
            _characterAnimator.Attack(true);
            Debug.Log("Attack!");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemy))
        {
            _characterAnimator.Attack(false);
        }
    }
}
