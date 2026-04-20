using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerAnimation _animation;

    private void OnEnable()
    {
        _input.Moved += _animation.PlayMove;
        _input.Jumped += _animation.PlayJump;
        _input.Attacked += _animation.PlayAttack;
        _movement.GroundedChanged += _animation.SetGrounded;
    }

    private void OnDisable()
    {
        _input.Moved -= _animation.PlayMove;
        _input.Jumped -= _animation.PlayJump;
        _input.Attacked -= _animation.PlayAttack;
        _movement.GroundedChanged -= _animation.SetGrounded;
    }
}