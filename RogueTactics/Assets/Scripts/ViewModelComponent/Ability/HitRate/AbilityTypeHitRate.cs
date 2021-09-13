using UnityEngine;

public class AbilityTypeHitRate : HitRate
{
    protected override int GetHitRate()
    {
        return GetComponentInParent<WeaponStats>()[WeaponStatTypes.HIT];
    }

    public override int Calculate(TileDefinitionData target)
    {
        Unit attacker = GetComponentInParent<Unit>();
        Unit defender = target.content.GetComponent<Unit>();
        
        if (AutomaticMiss(defender))
        {
            return Final(0);
        }

        if (AutomaticHit(defender))
        {
            return Final(100);
        }
        
        // Get the speed stat considering possible variations
        int speed = GetStat(attacker, defender, GetSpeedNotification, 0);
        
        // Get the evade stat considering possible variations
        int evade = GetStat(attacker, defender, GetLuckNotification, 0);
        
        // Get the skill stat considering possible variations
        int skill = GetStat(attacker, defender, GetSkillNotification, 0);
        
        // Get the luck stat considering possible variations
        int luck = GetStat(attacker, defender, GetLuckNotification, 0);
        
        // Get the weapon hit rate stat considering possible variations
        int weaponHitRate = GetStat(attacker, defender, GetHitRateNotification, 0);

        // Calculate base attack speed
        // TODO: add weapon weight and strength comparison
        int attackSpeed = speed;

        // Calculate base avoid
        // TODO: add terrain influence + supports ?
        int avoid = 2 * attackSpeed + evade;
        
        // Calculate base hit rate
        // TODO: add weapon level bonuses + supports ?
        int hitRate = (int)(2 * skill + 0.5 * luck + weaponHitRate - avoid);
        hitRate = Mathf.Clamp(hitRate, 1, 100);
        return Final(100 - hitRate);
    }
}
