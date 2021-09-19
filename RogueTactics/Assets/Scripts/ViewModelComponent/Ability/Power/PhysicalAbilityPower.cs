public class PhysicalAbilityPower : BaseAbilityPower
{
    public int might;

    protected override int GetBaseOffensiveStat()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.STR];
    }


    protected override int GetBaseDefensiveStat(Unit target)
    {
        return target.GetComponent<UnitStats>()[UnitStatTypes.DEF];
    }
    
    protected override int GetAbilityPower(Unit target)
    {
        return might;
    }
}
