using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class STypeHitRate : HitRate
{
    public override int Calculate(TileDefinitionData target)
    {
        Unit defender = target.content.GetComponent<Unit>();
        if (AutomaticMiss(defender))
        {
            return Final(100);
        }

        if (AutomaticHit(defender))
        {
            return Final(0);
        }

        int tenacity = GetResistance(defender);
        tenacity = AdjustForStatusEffects(defender, tenacity);
        tenacity = Mathf.Clamp(tenacity, 0, 100);
        return Final(tenacity);
    }

    private int GetResistance(Unit target)
    {
        Stats s = target.GetComponentInParent<Stats>();
        return s[StatTypes.TEN];
    }
}
