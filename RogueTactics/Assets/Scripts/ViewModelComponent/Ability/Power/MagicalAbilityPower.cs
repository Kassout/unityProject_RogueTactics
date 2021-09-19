public class MagicalAbilityPower : BaseAbilityPower
{
    public int might;

    protected override int GetBaseOffensiveStat()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.MAG];
    }

    protected override int GetBaseDefensiveStat(Unit target)
    {
        return target.GetComponent<UnitStats>()[UnitStatTypes.RES];
    }

    protected override int GetAbilityPower(Unit target)
    {
        return might;
    }
}
