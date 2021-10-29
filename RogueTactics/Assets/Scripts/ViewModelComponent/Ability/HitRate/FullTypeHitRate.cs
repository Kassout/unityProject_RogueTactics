public class FullTypeHitRate : HitRate
{
    public override int Calculate(WorldTile target)
    {
        Unit defender = target.content.GetComponent<Unit>();
        if (AutomaticMiss(defender))
        {
            return Final(100);
        }

        return Final(0);
    }
}
