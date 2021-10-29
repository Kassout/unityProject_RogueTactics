using UnityEngine;

public class HealAbilityEffect : BaseAbilityEffect
{
    public override int Predict(WorldTile target)
    {
        Unit attacker = GetComponentInParent<Unit>();
        Unit defender = target.content.GetComponent<Unit>();
        return GetStat(attacker, defender, GetAbilityPowerNotification, 0);
    }

    protected override int OnApply(WorldTile target)
    {
        Unit defender = target.content.GetComponent<Unit>();

        // Start with the predicted value
        int value = Predict(target);

        // Clamp the amount to a range
        value = Mathf.Clamp(value, minDamage, maxDamage);

        // Apply the amount to the target
        UnitStats s = defender.GetComponent<UnitStats>();
        s[UnitStatTypes.HP] += value;
        return value;
    }
}