public class EndTurnState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        owner.ChangeState<SelectUnitState>();
    }
}
