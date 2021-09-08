
public class EnemyAbilityEffectTarget : AbilityEffectTarget
{
    private Alliance _alliance;

    private void Start()
    {
        _alliance = GetComponentInParent<Alliance>();
    }

    public override bool IsTarget(TileDefinitionData tile)
    {
        if (tile == null || tile.content == null)
        {
            return false;
        }

        Alliance other = tile.content.GetComponentInChildren<Alliance>();
        return _alliance.IsMatch(other, Targets.Foe);
    }
}
