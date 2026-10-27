using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float _rayLength = 0.2f;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayLength, _groundMask);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * _rayLength);
    }
}
