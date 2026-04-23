using System;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public event Action Attacked;

    public void InvokeAttackingEvent() => Attacked?.Invoke();
}