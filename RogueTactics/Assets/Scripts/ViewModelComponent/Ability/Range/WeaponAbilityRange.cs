using System;
using UnityEngine;

public class WeaponAbilityRange : ConstantAbilityRange
{
    private void Start()
    {
        range = GetComponentInParent<WeaponStats>()[WeaponStatTypes.RAN];
    }
}