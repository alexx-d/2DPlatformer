using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EntityFlipper))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    protected Rigidbody2D Rigidbody;
    private EntityFlipper _flipper;

    public float Speed => _speed;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<EntityFlipper>();
    }

    public void Move(float direction)
    {
        Rigidbody.velocity = new Vector2(direction * _speed, Rigidbody.velocity.y);
        _flipper.FaceDirection(direction);
    }

    public void Stop()
    {
        Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
    }
}