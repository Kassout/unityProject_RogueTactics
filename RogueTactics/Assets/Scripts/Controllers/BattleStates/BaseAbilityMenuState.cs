using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseAbilityMenuState : BattleState
{
    protected string menuTitle;

    protected List<string> menuOptions;

    public override void Enter()
    {
        base.Enter();
        SelectTile(turn.actor.tile.position);

        if (_driver.Current == Drivers.Human)
        {
            LoadMenu();
        }
    }

    public override void Exit()
    {
        base.Exit();
        abilityMenuPanelController.Hide();
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
    }

    protected override void OnMovement(InputAction.CallbackContext context)
    {
    }

    protected override void OnCancel(InputAction.CallbackContext context)
    {
        Cancel();
    }

    protected abstract void LoadMenu();
    protected abstract void Confirm();
    protected abstract void Cancel();
}

