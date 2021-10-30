using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

/// <summary>
/// TODO: comments
/// </summary>
public class SelectUnitState : BattleState
{
    public override void Enter()
    {
        base.Enter();

        Cursor.visible = false;
        
        if (Turn.currentDriver == Drivers.Computer)
        {
            StartCoroutine(ComputerTurn());
        }
    }

    public override void Exit()
    {
        base.Exit();

        Cursor.visible = true;
    }

    private IEnumerator ComputerTurn()
    {
        List<Unit> validUnitList = Units
            .Where(unit => unit.GetComponent<Driver>().Current == Drivers.Computer && !unit.hasEndTurn).ToList();
        Turn.actor = validUnitList[new Random().Next(validUnitList.Count)]; 
        
        Turn.plan = owner.cpu.Evaluate();
        Turn.ability = Turn.plan.ability;
        Turn.targets = new List<WorldTile>() { Board.GetTile(Turn.plan.fireLocation) };

        yield return new WaitForSeconds(1f);

        if (Turn.plan.moveLocation != Turn.actor.tile.position)
        {
            owner.ChangeState<MoveTargetState>();
        }
        else
        {
            owner.ChangeState<ConfirmAbilityTargetState>();
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = BattleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        TileSelectionCursor.position =
            new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        GameObject content = null;

        var hit = Physics2D.Raycast(TileSelectionCursor.position, Vector2.zero);
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
        Turn.Change(Units[Units.FindIndex(unit => unit.Equals(target))]);
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}