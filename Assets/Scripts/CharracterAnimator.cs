using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharracterAnimator : MonoBehaviour
{
    private const string WalkParametrName = "isWalk";
    private const string JumpParametrName = "isJump";
    private const string AttackParametrName = "isAttack";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Walk(bool isActive)
    {
        _animator.SetBool(WalkParametrName, isActive);
    }

    public void Jump(bool isActive)
    {
        _animator.SetBool(JumpParametrName, isActive);
    }

    public void Attack(bool isActive)
    {
        _animator.SetBool(AttackParametrName, isActive);
    }
}