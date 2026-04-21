using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public event Action<Pickup> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}