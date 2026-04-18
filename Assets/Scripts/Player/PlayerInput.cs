using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimation))]
public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const KeyCode AttackKey = KeyCode.F;

    private float _horizontalInput;
    private float _lastHorizontalInput;
    private bool _jumpRequested;

    public event Action<float> Moved;
    public event Action Jumped;
    public event Action Attacked;

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw(Horizontal);
        _jumpRequested = Input.GetButtonDown(Jump);

        if (_horizontalInput != _lastHorizontalInput)
        {
            _lastHorizontalInput = _horizontalInput;
            Moved?.Invoke(_horizontalInput);
        }

        if (_jumpRequested)
        {
            Jumped?.Invoke();
        }

        if (Input.GetKeyDown(AttackKey))
        {
            Attacked?.Invoke();
        }
    }
}