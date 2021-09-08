using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CommandSelectionState : BaseAbilityMenuState
{
    public override void Enter()
    {
        base.Enter();

        if (_driver.Current == Drivers.Human)
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
            abilityMenuPanelController.PreviousMenuSelection();
        } 
        else if (axis < 0)
        {
            abilityMenuPanelController.NextMenuSelection();
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

        abilityMenuPanelController.Show(menuOptions);
        abilityMenuPanelController.SetLocked(0, turn.hasUnitActed);
        
        var ar = turn.actor.GetComponentInChildren<AbilityRange>().GetTilesInRange();
        turn.targets = units.Where(unit => ar.Any(tile => tile.position.Equals(unit.TileDefinition.position)) && !unit.Equals(turn.actor)).Select(unit => unit.TileDefinition).ToList();
        abilityMenuPanelController.SetLocked(0, !turn.targets.Any());
    }

    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0:
                Attack();
                break;
            case 1:
                owner.ChangeState<CategorySelectionState>();
                break;
            case 3:
                turn.actor.hasEndTurn = true;
                turn.actor.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }

    protected override void Cancel()
    {
        if (turn.hasUnitMoved && !turn.lockMove)
        {
            turn.UndoMove();
            abilityMenuPanelController.SetLocked(0, false);
            SelectTile(turn.actor.TileDefinition.position);
        }
        else
        {
            turn.UndoMove();
            SelectTile(turn.actor.TileDefinition.position); 
            owner.ChangeState<SelectUnitState>();
        }
    }
    
    void Attack ()
    {
        turn.ability = turn.actor.GetComponentInChildren<Ability>();
        owner.ChangeState<ConfirmAbilityTargetState>();
    }
}
