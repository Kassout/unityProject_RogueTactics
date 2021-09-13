using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAbilityPower : BaseAbilityPower
{
    public int level;

    protected override int GetBaseAttack()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.MAG];
    }

    protected override int GetBaseDefense(Unit target)
    {
        return target.GetComponent<UnitStats>()[UnitStatTypes.RES];
    }

    protected override int GetPower(Unit target)
    {
        return level;
    }
}
