using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CategorySelectionState : BaseAbilityMenuState
{
    public override void Enter()
    {
        base.Enter();
        
        inputManager.Cursor.Selection.performed += OnSelection;
        inputManager.Cursor.Selection.Enable();
        
        Cursor.visible = false;
    }

    public override void Exit()
    {
        base.Exit();
        
        inputManager.Cursor.Selection.performed -= OnSelection;
        inputManager.Cursor.Selection.Disable();
        
        Cursor.visible = true;
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

        AbilityCatalog catalog = turn.actor.GetComponentInChildren<AbilityCatalog>();
        for (int i = 0; i < catalog.CategoryCount(); ++i)
        {
            menuOptions.Add(catalog.GetCategory(i).name);
        }

        abilityMenuPanelController.Show(menuOptions);
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

    protected override void Confirm()
    {
        SetCategory(abilityMenuPanelController.selection);
    }

    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    void SetCategory(int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}