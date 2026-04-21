using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isGrounded;
    public bool IsGrounded => _isGrounded;

    public event Action<bool> GroundedChanged;

    private void FixedUpdate()
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);

        if (_isGrounded != wasGrounded)
        {
            GroundedChanged?.Invoke(_isGrounded);
        }
    }
}