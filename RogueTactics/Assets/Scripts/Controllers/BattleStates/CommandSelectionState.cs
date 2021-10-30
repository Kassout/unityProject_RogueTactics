using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandSelectionState : BaseAbilityMenuState
{
    public override void Enter()
    {
        base.Enter();

        if (driver.Current == Drivers.Human)
        {
            inputManager.Cursor.Selection.performed += OnSelection;
            inputManager.Cursor.Selection.Enable();
        
            Cursor.visible = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        inputManager.Cursor.Selection.performed -= OnSelection;
        inputManager.Cursor.Selection.Disable();
        
        Cursor.visible = true;
    }

    protected void OnSelection(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>().normalized * Time.fixedDeltaTime;

        if (context.performed)
        {
            StartCoroutine(nameof(ChangeSelection), mouseDelta.y);
        }
    }

    IEnumerator ChangeSelection(float axis)
    {
        if (axis > 0)
        {
            AbilityMenuPanelController.PreviousMenuSelection();
        } 
        else if (axis < 0)
        {
            AbilityMenuPanelController.NextMenuSelection();
        }

        inputManager.Cursor.Selection.Disable();

        yield return new WaitForSeconds(0.2f);
        
        inputManager.Cursor.Selection.Enable();
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        Confirm();
    }

    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuOptions = new List<string>();
        }
        else
        {
            menuOptions.Clear();
        }
        
        menuOptions.Add("Attack");
        menuOptions.Add("Abilities");
        menuOptions.Add("Objects");
        menuOptions.Add("Wait");

        AbilityMenuPanelController.Show(menuOptions);
        AbilityMenuPanelController.SetLocked(0, Turn.hasUnitActed);
        
        var ar = Turn.actor.GetComponentInChildren<AbilityRange>().GetTilesInRange();
        Turn.targets = Units.Where(unit => ar.Any(tile => tile.position.Equals(unit.tile.position)) && !unit.Equals(Turn.actor)).Select(unit => unit.tile).ToList();
        AbilityMenuPanelController.SetLocked(0, !Turn.targets.Any());
    }

    protected override void Confirm()
    {
        switch (AbilityMenuPanelController.selection)
        {
            case 0:
                Attack();
                break;
            case 1:
                owner.ChangeState<CategorySelectionState>();
                break;
            case 3:
                Turn.actor.hasEndTurn = true;
                Turn.actor.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                owner.ChangeState<TurnManagerState>();
                break;
        }
    }

    protected override void Cancel()
    {
        if (Turn.hasUnitMoved && !Turn.lockMove)
        {
            Turn.UndoMove();
            AbilityMenuPanelController.SetLocked(0, false);
            SelectTile(Turn.actor.tile.position);
        }
        else
        {
            Turn.UndoMove();
            SelectTile(Turn.actor.tile.position); 
            owner.ChangeState<SelectUnitState>();
        }
    }
    
    void Attack ()
    {
        Turn.ability = Turn.actor.GetComponentInChildren<Ability>();
        owner.ChangeState<ConfirmAbilityTargetState>();
    }
}
