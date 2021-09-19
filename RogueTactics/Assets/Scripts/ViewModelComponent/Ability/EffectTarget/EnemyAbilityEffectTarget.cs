
public class EnemyAbilityEffectTarget : AbilityEffectTarget
{
    private Alliance _alliance;

    private void Start()
    {
        _alliance = GetComponentInParent<Alliance>();
    }

    public override bool IsTarget(WorldTile worldTile)
    {
        if (worldTile == null || worldTile.content == null)
        {
            return false;
        }

        Alliance other = worldTile.content.GetComponentInChildren<Alliance>();
        return _alliance.IsMatch(other, Targets.Foe);
    }
}
