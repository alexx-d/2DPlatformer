using System;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public event Action<Collectable> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}