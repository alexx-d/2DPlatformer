using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class EnemyAnimation : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float currentSpeed = Mathf.Abs(_rigidbody.velocity.x);

        _animator.SetFloat(SpeedHash, currentSpeed);
    }
}