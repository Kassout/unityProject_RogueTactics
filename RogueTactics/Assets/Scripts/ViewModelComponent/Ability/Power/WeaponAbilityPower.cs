using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAbilityPower : BaseAbilityPower
{
    protected override int GetBaseAttack ()
    {
        return GetComponentInParent<Stats>()[StatTypes.STR];
    }
    protected override int GetBaseDefense (Unit target)
    {
        return target.GetComponent<Stats>()[StatTypes.DEF];
    }
  
    protected override int GetPower ()
    {
        int power = PowerFromEquippedWeapon();
        return power > 0 ? power : UnarmedPower();
    }
    int PowerFromEquippedWeapon ()
    {
        int power = 0;
        Equipment eq = GetComponentInParent<Equipment>();
        Equippable item = eq.GetItem(EquipSlots.Weapon);
        StatModifierFeature[] features = item.GetComponentsInChildren<StatModifierFeature>();
        for (int i = 0; i < features.Length; ++i)
        {
            if (features[i].type == StatTypes.STR)
                power += features[i].amount;
        }
    
        return power;
    }
    int UnarmedPower ()
    {
        Class job = GetComponentInParent<Class>();
        for (int i = 0; i < Class.statOrder.Length; ++i)
        {
            if (Class.statOrder[i] == StatTypes.STR)
                return job.baseStats[i];
        }
        return 0;
    }
}
