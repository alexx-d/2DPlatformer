using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingSaw : MonoBehaviour
{
    [SerializeField] private Vector2 _moveDirection = Vector2.right;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _damage = 10;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _startPosition = transform.position;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float moveProgress = Mathf.PingPong(Time.time * _speed, _distance);

        Vector2 targetPosition = _startPosition + (_moveDirection.normalized * moveProgress);
        _rigidbody.MovePosition(targetPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
        }
    }
}