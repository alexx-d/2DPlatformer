using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private MeleeAttacker _attacker;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimation _animation;

    public Health Health => _health;
    public PlayerWallet Wallet => _wallet;
    public PlayerMovement Movement => _movement;
    public PlayerAnimation Animation => _animation;

    private void OnEnable()
    {
        _input.Moved += _movement.SetDirection;
        _input.Moved += _animation.PlayMove;

        _input.Jumped += OnJumpRequested;

        _input.Attacked += _attacker.Attack;
        _input.Attacked += _animation.PlayAttack;

        _groundDetector.GroundedChanged += _animation.SetGrounded;
    }

    private void OnDisable()
    {
        _input.Moved -= _movement.SetDirection;
        _input.Moved -= _animation.PlayMove;

        _input.Jumped -= OnJumpRequested;

        _input.Attacked -= _attacker.Attack;
        _input.Attacked -= _animation.PlayAttack;

        _groundDetector.GroundedChanged -= _animation.SetGrounded;
    }

    private void OnJumpRequested()
    {
        if (_groundDetector.IsGrounded)
        {
            _movement.Jump();
            _animation.PlayJump();
        }
    }
}