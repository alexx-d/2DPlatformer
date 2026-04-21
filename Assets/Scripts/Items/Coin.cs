using System;
using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] private int _amount = 1;
    public int Amount => _amount;
}