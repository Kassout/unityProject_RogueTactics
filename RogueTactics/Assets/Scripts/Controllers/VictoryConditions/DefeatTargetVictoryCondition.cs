public class DefeatTargetVictoryCondition : BaseVictoryCondition
{
    public Unit target;

    protected override void CheckForGameOver()
    {   
        base.CheckForGameOver();
        if (Victory == Alliances.None && IsDefeated(target))
        {
            Victory = Alliances.Hero;
        }
    }
}
