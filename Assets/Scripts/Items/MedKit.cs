using System;
using UnityEngine;

public class MedKit : Collectable
{
    [SerializeField] private int _healPower = 25;
    public int HealPower => _healPower;
}