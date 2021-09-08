using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATypeHitRate : HitRate
{
    public override int Calculate(TileDefinitionData target)
    {
        Unit defender = target.content.GetComponent<Unit>();
        if (AutomaticMiss(defender))
        {
            return Final(0);
        }

        if (AutomaticHit(defender))
        {
            return Final(100);
        }

        int evade = GetEvade(defender);
        evade = AdjustForStatusEffects(defender, evade);
        evade = Mathf.Clamp(evade, 1, 100);
        return Final(evade);
    }

    private int GetEvade(Unit target)
    {
        Stats s = target.GetComponentInParent<Stats>();
        return Mathf.Clamp(s[StatTypes.EVD], 0, 100);
    }
}
