using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int IsAngryHash = Animator.StringToHash("IsAngry");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(SpeedHash, speed);
    }

    public void SetAggressive(bool isAggressive)
    {
        _animator.SetBool(IsAngryHash, isAggressive);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(AttackHash);
    }
}