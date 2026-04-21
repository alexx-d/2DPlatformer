using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EntityFlipper))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 2f;

    private Rigidbody2D _rigidbody;
    private EntityFlipper _flipper;

    private float _direction;
    private bool _isJumpRequested;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<EntityFlipper>();
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);

        _flipper.FaceDirection(_direction);

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

    public void Jump()
    {
        _isJumpRequested = true;
    }
}