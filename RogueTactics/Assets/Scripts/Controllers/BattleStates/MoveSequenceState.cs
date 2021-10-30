using System.Collections;

/// <summary>
/// TODO: comments
/// </summary>
public class MoveSequenceState : BattleState
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        StartCoroutine("Sequence");
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    private IEnumerator Sequence()
    {
        var m = owner.turn.actor.GetComponent<UnitMovement>();
        BattleController.Instance.inTransition = true;
        yield return StartCoroutine(m.Traverse(CurrentSelectedWorldTile));
        BattleController.Instance.inTransition = false;
        if (driver.Current == Drivers.Human)
        {
            if (Turn.ability != null && Turn.targets.Count != 0)
            {
                owner.ChangeState<ConfirmAbilityTargetState>();
            }
            else
            {
                owner.ChangeState<CommandSelectionState>();
            }
        }
        else
        {
            if (Turn.ability != null)
            {
                owner.battleMessageController.Display(Turn.ability.name);
                owner.ChangeState<PerformAbilityState>();
            }
            else
            {
                owner.ChangeState<TurnManagerState>();
            }
        }

    }
}