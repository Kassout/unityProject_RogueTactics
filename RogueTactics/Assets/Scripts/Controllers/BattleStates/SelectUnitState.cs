using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

/// <summary>
///     TODO: comments
/// </summary>
public class SelectUnitState : BattleState
{
    public override void Enter()
    {
        base.Enter();

        if (turn.currentDriver == Drivers.Computer)
        {
            StartCoroutine(ComputerTurn());
        }
    }

    private IEnumerator ComputerTurn()
    {
        List<Unit> validUnitList = units
            .Where(unit => unit.GetComponent<Driver>().Current == Drivers.Computer && !unit.hasEndTurn).ToList();
        turn.actor = validUnitList[new Random().Next(validUnitList.Count)]; 
        
        turn.plan = owner.cpu.Evaluate();
        turn.ability = turn.plan.ability;

        yield return new WaitForSeconds(1f);

        if (turn.plan.moveLocation != turn.actor.TileDefinition.position)
        {
            owner.ChangeState<MoveTargetState>();
        }
        else
        {
            owner.ChangeState<ConfirmAbilityTargetState>();
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

            if (content.GetComponent<Unit>() && !content.GetComponent<Unit>().hasEndTurn &&
                content.GetComponent<Driver>() && content.GetComponent<Driver>().Current == Drivers.Human)
            {
                StartCoroutine(nameof(ChangeCurrentUnit), content.GetComponent<Unit>());
            }
        }
        else
        {
            Debug.Log("No hit");
        }
    }

    private IEnumerator ChangeCurrentUnit(Unit target)
    {
        turn.Change(units[units.FindIndex(unit => unit.Equals(target))]);
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}