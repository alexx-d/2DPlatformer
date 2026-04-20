using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
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
    private PlayerInput _playerInput;
    private Vector3 _initialScale;

    private float _direction;
    private bool _isJumpRequested;
    private bool _isGrounded;

    public event Action<bool> GroundedChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        _playerInput.Moved += OnPlayerMoved;
        _playerInput.Jumped += OnPlayerJumped;
    }

    private void OnDisable()
    {
        _playerInput.Moved -= OnPlayerMoved;
        _playerInput.Jumped -= OnPlayerJumped;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = IsGrounded();

        if (_isGrounded != wasGrounded)
        {
            GroundedChanged?.Invoke(_isGrounded);
        }

        _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);

        UpdateFacingDirection();

        if (_isJumpRequested)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isJumpRequested = false;
        }
    }

    private void OnPlayerMoved(float direction)
    {
        _direction = direction;
    }

    private void OnPlayerJumped()
    {
        if (_isGrounded)
        {
            _isJumpRequested = true;
        }
    }

    private bool IsGrounded()
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