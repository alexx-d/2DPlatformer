using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 2f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private const float RightDirection = 1f;
    private const float LeftDirection = -1f;

    private Rigidbody2D _rigidbody;
    private Vector3 _initialScale;

    private float _direction;
    private bool _isJumpRequested;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _initialScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGround();

        _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);

        UpdateFacingDirection();

        if (_isJumpRequested)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isJumpRequested = false;
        }
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
    }

    public void TryJump()
    {
        if (_isGrounded)
        {
            _isJumpRequested = true;
        }
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
    }

    private void UpdateFacingDirection()
    {
        if (_direction == 0)
        {
            return;
        }

        float lookDirection = _direction > 0 ? RightDirection : LeftDirection;

        transform.localScale = new Vector3(
            _initialScale.x * lookDirection,
            _initialScale.y,
            _initialScale.z
        );
    }
}