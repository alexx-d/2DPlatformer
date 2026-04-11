using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    private PlayerMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw(Horizontal);
        bool jumpRequested = Input.GetButtonDown(Jump);

        _movement.SetDirection(horizontal);

        if (jumpRequested)
        {
            _movement.TryJump();
        }
    }
}