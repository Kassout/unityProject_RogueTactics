using System;
using UnityEngine;

public class WeaponAbilityRange : ConstantAbilityRange
{
    private void Start()
    {
        horizontal = GetComponentInParent<WeaponStats>()[WeaponStatTypes.RAN];
    }
}