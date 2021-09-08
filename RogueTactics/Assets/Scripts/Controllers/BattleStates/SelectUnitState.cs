using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     TODO: comments
/// </summary>
public class SelectUnitState : BattleState
{
    public override void Enter()
    {
        base.Enter();

        if (_driver.Current == Drivers.Computer)
        {
            StartCoroutine(ComputerTurn());
        }
    }

    private IEnumerator ComputerTurn()
    {
        if (turn.plan == null)
        {
            turn.plan = owner.cpu.Evaluate();
            turn.ability = turn.plan.ability;
        }

        yield return new WaitForSeconds(1f);

        if (turn.hasUnitMoved == false && turn.plan.moveLocation != turn.actor.TileDefinition.position)
        {
            owner.ChangeState<MoveTargetState>();
        }
        else
        {
            owner.ChangeState<EndTurnState>();
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        tileSelectionCursor.position =
            new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        GameObject content = null;

        var hit = Physics2D.Raycast(tileSelectionCursor.position, Vector2.zero);
        if (hit.transform != null)
        {
            Debug.Log("Hit " + hit.transform.gameObject.name);
            content = hit.transform.gameObject;
        }
        else
        {
            Debug.Log("No hit");
        }

        if (content != null && units.Contains(content.GetComponent<Unit>()) && !content.GetComponent<Unit>().hasEndTurn)
        {
            StartCoroutine("ChangeCurrentUnit", content.GetComponent<Unit>());
        }
    }

    private IEnumerator ChangeCurrentUnit(Unit target)
    {
        _driver = (turn.actor != null) ? turn.actor.GetComponent<Driver>() : null;
        turn.Change(units[units.FindIndex(unit => unit.Equals(target))]);
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}