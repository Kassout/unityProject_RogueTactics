using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAbilityPower : BaseAbilityPower
{
    public int might;

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
        return might;
    }
}
