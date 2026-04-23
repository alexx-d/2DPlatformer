using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : Mover
{
    [SerializeField] private float _stopThreshold = 0.1f;

    private const float RightDirection = 1f;
    private const float LeftDirection = -1f;

    private Vector2 _targetPosition;
    private bool _hasTarget;

    private void FixedUpdate()
    {
        if (_hasTarget == false)
        {
            return;
        }

        float distanceToTarget = _targetPosition.x - transform.position.x;

        if (Mathf.Abs(distanceToTarget) < _stopThreshold)
        {
            Stop();
            return;
        }

        float direction = distanceToTarget > 0 ? RightDirection : LeftDirection;
        Move(direction);
    }

    public void SetTarget(Vector2 target)
    {
        _targetPosition = target;
        _hasTarget = true;
    }
}