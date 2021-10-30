using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// TODO: comments
/// </summary>
public class ActionSelectionState : BaseAbilityMenuState
{
    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    public static int category;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    private AbilityCatalog _catalog;

    #endregion

    #region Private

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="axis">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    private IEnumerator ChangeSelection(float axis)
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

    #endregion

    #region Protected
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected void OnSelection(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>().normalized * Time.fixedDeltaTime;

        if (context.performed)
        {
            StartCoroutine(nameof(ChangeSelection), mouseDelta.y);
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void LoadMenu()
    {
        _catalog = Turn.actor.GetComponentInChildren<AbilityCatalog>();
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

        AbilityMenuPanelController.Show(menuOptions);
        for (int i = 0; i < count; ++i)
        {
            AbilityMenuPanelController.SetLocked(i, locks[i]);
        }
    }
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        Confirm();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void Confirm()
    {
        Turn.ability = _catalog.GetAbility(category, AbilityMenuPanelController.selection);
        owner.ChangeState<AbilityTargetState>();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }

    #endregion

    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        inputManager.Cursor.Selection.performed += OnSelection;
        inputManager.Cursor.Selection.Enable();

        Cursor.visible = false;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        inputManager.Cursor.Selection.performed -= OnSelection;
        inputManager.Cursor.Selection.Disable();

        Cursor.visible = true;
    }

    #endregion
}