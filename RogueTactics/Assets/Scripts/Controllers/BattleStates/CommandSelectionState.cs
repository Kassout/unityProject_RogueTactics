using System.Collections;
using System.Collections.Generic;
using BattleStates;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandSelectionState : BaseAbilityMenuState
{
    private Vector2 _lastMousePos = Vector2.zero;

    public override void Enter()
    {
        base.Enter();
        Cursor.visible = false;
    }

    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 loop = Vector2.zero;
        var mouse = Mouse.current;
        if (mouse != null)
        {
            loop = mouse.delta.ReadValue();
        }
        
        if (loop.y > 3)
        {
            abilityMenuPanelController.Next();
        } else if (loop.y < -3)
        {
            abilityMenuPanelController.Previous();
        }
    }

    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuOptions = new List<string>(3);
            menuOptions.Add("Action");
            menuOptions.Add("Wait");
        }
        
        abilityMenuPanelController.Show(menuOptions);
        abilityMenuPanelController.SetLocked(0, turn.hasUnitActed);
    }

    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0:
                owner.ChangeState<CategorySelectionState>();
                break;
            case 1:
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
}
