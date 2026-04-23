using UnityEngine;

public class PlayerMovement : Mover
{
    [SerializeField] private float _jumpForce = 5f;

    private float _inputDirection;
    private bool _isJumpRequested;

    private void FixedUpdate()
    {
        Move(_inputDirection);

        if (_isJumpRequested)
        {
            Rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isJumpRequested = false;
        }
    }

    public void SetDirection(float direction)
    {
        _inputDirection = direction;
    }

    public void Jump()
    {
        _isJumpRequested = true;
    }
}