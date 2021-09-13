using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOdAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(TileDefinitionData tile)
    {
        if (tile == null || tile.content == null)
        {
            return false;
        }

        UnitStats s = tile.content.GetComponent<UnitStats>();
        return s != null && s[UnitStatTypes.HP] <= 0;
    }
}
