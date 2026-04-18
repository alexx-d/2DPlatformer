using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerInput), typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _playerInput.Moved += OnPlayerMoved;
        _playerInput.Jumped += OnPlayerJumped;
        _playerInput.Attacked += OnPlayerAttacked;
        _playerMovement.GroundedChanged += OnGroundedChanged;
    }

    private void OnDisable()
    {
        _playerInput.Moved -= OnPlayerMoved;
        _playerInput.Jumped -= OnPlayerJumped;
        _playerInput.Attacked -= OnPlayerAttacked;
        _playerMovement.GroundedChanged -= OnGroundedChanged;
    }

    public void OnPlayerMoved(float value)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(value));
    }

    public void OnPlayerJumped()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    public void OnPlayerAttacked()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
    }

    private void OnGroundedChanged(bool isGrounded)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
    }
}