using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAbilityPower : BaseAbilityPower
{
    public int level;

    protected override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.MAG];
    }

    protected override int GetBaseDefense(Unit target)
    {
        return target.GetComponent<Stats>()[StatTypes.RES];
    }

    protected override int GetPower()
    {
        return level;
    }
}
