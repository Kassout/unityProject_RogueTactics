using System.Collections;
using UnityEngine.InputSystem;

/// <summary>
///     TODO: comments
/// </summary>
public class MoveSequenceState : BattleState
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        StartCoroutine("Sequence");
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    private IEnumerator Sequence()
    {
        var m = owner.turn.actor.GetComponent<UnitMovement>();
        yield return StartCoroutine(m.Traverse(currentSelectedTile));
        if (_driver.Current == Drivers.Human)
        {
            owner.ChangeState<CommandSelectionState>();
        }
        else
        {
            if (turn.ability != null)
            {
                owner.battleMessageController.Display(turn.ability.name);
                owner.ChangeState<PerformAbilityState>();
            }
            else
            {
                owner.ChangeState<TurnManagerState>();
            }

        }

    }
}