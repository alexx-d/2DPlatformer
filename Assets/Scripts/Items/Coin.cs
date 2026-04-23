using System;
using UnityEngine;

public class Coin : Collectable
{
    [SerializeField] private int _amount = 1;
    public int Amount => _amount;
}