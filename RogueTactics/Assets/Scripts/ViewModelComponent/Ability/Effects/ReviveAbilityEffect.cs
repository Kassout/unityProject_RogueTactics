using UnityEngine;
using System.Collections;

public class ReviveAbilityEffect : BaseAbilityEffect
{
    public float percent;

    public override int Predict(TileDefinitionData target)
    {
        UnitStats s = target.content.GetComponent<UnitStats>();
        return Mathf.FloorToInt(s[UnitStatTypes.MHP] * percent);
    }

    protected override int OnApply(TileDefinitionData target)
    {
        UnitStats s = target.content.GetComponent<UnitStats>();
        int value = s[UnitStatTypes.HP] = Predict(target);
        return value;
    }
}