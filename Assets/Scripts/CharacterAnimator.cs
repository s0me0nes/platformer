using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private const string WalkParametrName = "isWalk";
    private const string JumpParametrName = "Jump";
    private const string AttackParametrName = "Attack";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Walk(bool isActive)
    {
        _animator.SetBool(WalkParametrName, isActive);
    }

    public void Jump()
    {
        _animator.SetTrigger(JumpParametrName);
    }

    public void Attack()
    {
        _animator.SetTrigger(AttackParametrName);
    }

    public void SetFacing(float direction)
    {
        if (direction == 0) 
            return;

        transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}