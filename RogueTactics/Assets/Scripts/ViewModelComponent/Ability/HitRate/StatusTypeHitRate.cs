using UnityEngine;

public class StatusTypeHitRate : HitRate
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
        UnitStats s = target.GetComponentInParent<UnitStats>();
        return s[UnitStatTypes.TEN];
    }
}
