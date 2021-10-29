using UnityEngine;

public class ReviveAbilityEffect : BaseAbilityEffect
{
    public float percent;

    public override int Predict(WorldTile target)
    {
        UnitStats s = target.content.GetComponent<UnitStats>();
        return Mathf.FloorToInt(s[UnitStatTypes.MHP] * percent);
    }

    protected override int OnApply(WorldTile target)
    {
        UnitStats s = target.content.GetComponent<UnitStats>();
        int value = s[UnitStatTypes.HP] = Predict(target);
        return value;
    }
}