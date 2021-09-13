using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAbilityPower : BaseAbilityPower
{
    private Weapons _weaponsCat;
    
    protected override int GetBaseAttack()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.STR];
    }

    protected override int GetBaseDefense(Unit target)
    {
        return target.GetComponent<UnitStats>()[UnitStatTypes.DEF];
    }

    protected override int GetPower(Unit target)
    {
        int power = GetComponentInParent<WeaponStats>()[WeaponStatTypes.POW];
        
        Equipment eq = GetComponentInParent<Equipment>();
        Weapon weapon = (Weapon)eq.GetItem(EquipSlots.Weapon);
            
        Equipment targetEq = target.GetComponentInChildren<Equipment>();
        Weapon targetWeapon = (Weapon) targetEq.GetItem(EquipSlots.Weapon);

        if (weapon.weaponCat != Weapons.None && targetWeapon.weaponCat != Weapons.None)
        {
            power += CheckWeaponTriangleBonus(weapon, targetWeapon);
        }
        
        return power;
    }

    int CheckWeaponTriangleBonus(Weapon attackerWeapon, Weapon defenderWeapon)
    {
        return attackerWeapon.CheckWeaponAdvantage(defenderWeapon);
    }

    int UnarmedPower()
    {
        Class job = GetComponentInParent<Class>();
        for (int i = 0; i < Class.StatOrder.Length; ++i)
        {
            if (Class.StatOrder[i] == UnitStatTypes.STR)
                return job.baseStats[i];
        }

        return 0;
    }
}