using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private PlayerMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, _movement.IsGrounded);
    }
}