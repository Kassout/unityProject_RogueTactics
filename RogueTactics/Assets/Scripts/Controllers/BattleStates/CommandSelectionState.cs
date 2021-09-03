using System.Collections;
using System.Collections.Generic;
using BattleStates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CommandSelectionState : BaseAbilityMenuState
{
    private Vector2 _lastMousePos = Vector2.zero;

    public override void Enter()
    {
        base.Enter();
        
        _inputManager.Cursor.Selection.performed += OnSelection;
        _inputManager.Cursor.Selection.Enable();
        
        Cursor.visible = false;
    }

    public override void Exit()
    {
        base.Exit();
        
        _inputManager.Cursor.Selection.performed -= OnSelection;
        _inputManager.Cursor.Selection.Disable();
        
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

        _inputManager.Cursor.Selection.Disable();

        yield return new WaitForSeconds(0.2f);
        
        _inputManager.Cursor.Selection.Enable();
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        Confirm();
    }

    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuOptions = new List<string>(4);
            menuOptions.Add("Attack");
            menuOptions.Add("Abilities");
            menuOptions.Add("Objects");
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
                Attack();
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
    
    void Attack ()
    {
        turn.ability = turn.actor.GetComponentInChildren<AbilityRange>().gameObject;
        owner.ChangeState<AbilityTargetState>();
    }
}
