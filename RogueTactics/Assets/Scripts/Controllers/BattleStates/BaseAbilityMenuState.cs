using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// TODO: comments
/// </summary>
public abstract class BaseAbilityMenuState : BattleState
{
    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected string menuTitle;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected List<string> menuOptions;

    #endregion

    #region Protected

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnInteraction(InputAction.CallbackContext context) {}

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnMovement(InputAction.CallbackContext context) {}

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnCancel(InputAction.CallbackContext context)
    {
        Cancel();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected abstract void LoadMenu();
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    protected abstract void Confirm();
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    protected abstract void Cancel();

    #endregion

    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        SelectTile(Turn.actor.tile.position);

        if (driver.Current == Drivers.Human)
        {
            LoadMenu();
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        AbilityMenuPanelController.Hide();
    }

    #endregion
}

