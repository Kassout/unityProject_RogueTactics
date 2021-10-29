public class KOdAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(WorldTile worldTile)
    {
        if (worldTile == null || worldTile.content == null)
        {
            return false;
        }

        UnitStats s = worldTile.content.GetComponent<UnitStats>();
        return s != null && s[UnitStatTypes.HP] <= 0;
    }
}
