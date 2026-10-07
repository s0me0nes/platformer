using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damageForce = 5;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _searchTimer = 1f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _searchingRadius = 1f;
    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void Start()
    {
        if (_attackPoint != null)
        {
            StartCoroutine(ContactSearch());
        }
    }

    private IEnumerator ContactSearch()
    {
        var timer = new WaitForSeconds(_searchTimer);

        while (true)
        {
            Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, _searchingRadius, _playerMask);

            if (hit != null)
            {
                Attack(hit);
            }

            yield return timer;
        }
    }

    private void Attack(Collider2D hit)
    {
        _characterAnimator.Attack();

        if (hit.TryGetComponent<IDamageable>(out var damagable))
        {
            damagable.TakeDamage(_damageForce);
        }
    }
}
