using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
public class EnemyVision : MonoBehaviour
{
    public bool IsPlayerVisible { get; private set; }

    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _distanceToTarget = 4f;
    [SerializeField] private float _visionTimer = 1f;

    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void Start()
    {
        StartCoroutine(CheckVisibleZone());
    }


    private IEnumerator CheckVisibleZone()
    {
        var timer = new WaitForSeconds(_visionTimer);


        while (true)
        {
            yield return timer;

            float direction = _enemyPatrol.Direction;

            if (direction == 1)
            {
                FindPlayer(Vector2.right);
            }
            else if (direction == -1)
            {
                FindPlayer(Vector2.left);
            }
            else
            {
                IsPlayerVisible = false;
            }
        }
    }

    private void FindPlayer(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _distanceToTarget, _playerMask);

        IsPlayerVisible = hit.collider != null;
    }
}