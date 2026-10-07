using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange = 4;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damageForce = 2;

    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (_attackPoint == null)
            return;

        _characterAnimator.Attack();

        Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _enemyMask);

        if (hit == null)
            return;

        if (hit.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_damageForce);
        }
    }
}