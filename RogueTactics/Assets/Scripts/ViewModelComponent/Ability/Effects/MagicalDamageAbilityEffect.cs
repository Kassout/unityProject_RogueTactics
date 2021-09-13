using UnityEngine;

public class MagicalDamageAbilityEffect : BaseAbilityEffect
{
    #region Public
    public override int Predict (TileDefinitionData target)
    {
        Unit attacker = GetComponentInParent<Unit>();
        Unit defender = target.content.GetComponent<Unit>();

        // Get the abilities power stat considering possible variations
        // TODO : add weapon effectiveness
        int power = GetStat(attacker, defender, GetPowerNotification, 0);
        
        // Get the targets base defense stat considering
        // mission items, support check, status check, and equipment, etc
        int defense = GetStat(attacker, defender, GetDefenseNotification, 0);
        
        // Get the attackers base attack stat considering
        // mission items, support check, status check, and equipment, etc
        int strength = GetStat(attacker, defender, GetStrengthNotification, 0);

        // Calculate base defense damage
        // TODO : add terrain modifiers + supports ?
        int effectiveDefense = defense;
        
        // Calculate base damage
        int damage = strength + power - effectiveDefense;
        damage = Mathf.Max(damage, 1);
        
        // Tweak the damage based on a variety of other checks like
        // Elemental damage, Critical Hits, Damage multipliers, etc.
        damage = GetStat(attacker, defender, TweakDamageNotification, damage);

        // Clamp the damage to a range
        damage = Mathf.Clamp(damage, minDamage, maxDamage);
        return -damage;
    }
	
    protected override int OnApply (TileDefinitionData target)
    {
        Unit defender = target.content.GetComponent<Unit>();

        // Start with the predicted damage value
        int value = Predict(target);

        // Clamp the damage to a range
        value = Mathf.Clamp(value, minDamage, maxDamage);

        // Apply the damage to the target
        UnitStats s = defender.GetComponent<UnitStats>();
        s[UnitStatTypes.HP] += value;
        return value;
    }
    #endregion
}