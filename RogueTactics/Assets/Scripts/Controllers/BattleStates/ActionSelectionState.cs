using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    private AbilityCatalog _catalog;

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
        _catalog = turn.actor.GetComponentInChildren<AbilityCatalog>();
        GameObject container = _catalog.GetCategory(category);

        int count = _catalog.AbilityCount(container);
        if (menuOptions == null)
        {
            menuOptions = new List<string>(count);
        }
        else
        {
            menuOptions.Clear();
        }

        bool[] locks = new bool[count];
        for (int i = 0; i < count; ++i)
        {
            Ability ability = _catalog.GetAbility(category, i);
            AbilityMagicCost cost = ability.GetComponent<AbilityMagicCost>();
            if (cost)
            {
                menuOptions.Add(string.Format("{0}: {1}", ability.name, cost.amount));
            }
            else
            {
                menuOptions.Add(ability.name);
            }

            locks[i] = !ability.CanPerform();
        }

        abilityMenuPanelController.Show(menuOptions);
        for (int i = 0; i < count; ++i)
        {
            abilityMenuPanelController.SetLocked(i, locks[i]);
        }
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
        turn.ability = _catalog.GetAbility(category, abilityMenuPanelController.selection);
        owner.ChangeState<AbilityTargetState>();
    }

    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }
}