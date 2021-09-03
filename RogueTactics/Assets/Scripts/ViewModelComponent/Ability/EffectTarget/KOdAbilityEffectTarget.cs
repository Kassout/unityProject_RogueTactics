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

        Stats s = tile.content.GetComponent<Stats>();
        return s != null && s[StatTypes.HP] <= 0;
    }
}
