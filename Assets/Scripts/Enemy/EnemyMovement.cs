using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private const float RightDirection = 1f;
    private const float LeftDirection = -1f;
    private const float StopThreshold = 0.1f;

    private Rigidbody2D _rigidbody;
    private Vector3 _initialScale;
    private Vector2 _targetPosition;
    private bool _hasTarget;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _initialScale = transform.localScale;
    }

    public void SetTarget(Vector2 target)
    {
        _targetPosition = target;
        _hasTarget = true;
    }

    private void FixedUpdate()
    {
        if (_hasTarget == false)
        {
            return;
        }

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        float distanceToTarget = _targetPosition.x - transform.position.x;

        float direction = distanceToTarget > 0 ? RightDirection : LeftDirection;

        if (Mathf.Abs(distanceToTarget) < StopThreshold)
        {
            StopHorizontalMovement();
            return;
        }

        ApplyVelocity(direction);
        UpdateFacingDirection(direction);
    }

    private void ApplyVelocity(float direction)
    {
        _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);
    }

    private void StopHorizontalMovement()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    private void UpdateFacingDirection(float direction)
    {
        transform.localScale = new Vector3(
            _initialScale.x * direction,
            _initialScale.y,
            _initialScale.z
        );
    }
}