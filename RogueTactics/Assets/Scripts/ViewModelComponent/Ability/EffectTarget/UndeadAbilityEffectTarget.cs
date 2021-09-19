using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadAbilityEffectTarget : AbilityEffectTarget
{
    /// <summary>
    /// Indicates whether the Undead component must be present (true)
    /// or must not be present (false) for the target to be valid.
    /// </summary>
    public bool toggle;

    public override bool IsTarget(WorldTile worldTile)
    {
        if (worldTile == null || worldTile.content == null)
            return false;
        bool hasComponent = worldTile.content.GetComponent<Undead>() != null;
        if (hasComponent != toggle)
            return false;

        UnitStats s = worldTile.content.GetComponent<UnitStats>();
        return s != null && s[UnitStatTypes.HP] > 0;
    }
}