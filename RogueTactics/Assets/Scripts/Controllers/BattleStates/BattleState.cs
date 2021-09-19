using System.Collections.Generic;
using Common.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     TODO: comments
/// </summary>
public class BattleState : State
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    protected InputManager inputManager;

    protected Driver _driver;
    
    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected BattleController owner;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected Camera battleCamera => owner.battleCamera;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected Transform tileSelectionCursor => owner.tileSelectionCursor;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected WorldTile currentSelectedWorldTile => owner.currentSelectedWorldTile;

    public AbilityMenuPanelController abilityMenuPanelController => owner.abilityMenuPanelController;

    public StatPanelController statPanelController => owner.statPanelController;

    public HitSuccessIndicator hitSuccessIndicator => owner.hitSuccessIndicator;

    public Turn turn => owner.turn;

    public List<Unit> units => owner.units;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private Vector2 position
    {
        get => owner.position;
        set => owner.position = value;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
        inputManager = new InputManager();
    }
    
    public override void Enter ()
    {
        _driver = (turn.actor != null) ? turn.actor.GetComponent<Driver>() : null;
        base.Enter ();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected override void AddListeners()
    {
        if (turn.currentDriver == Drivers.None || turn.currentDriver == Drivers.Human)
        {
            inputManager.Cursor.Movement.performed += OnMovement;
            inputManager.Cursor.Movement.Enable();

            inputManager.Cursor.Interaction.performed += OnInteraction;
            inputManager.Cursor.Interaction.Enable();

            inputManager.Cursor.Cancel.performed += OnCancel;
            inputManager.Cursor.Cancel.Enable();
        }
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected override void RemoveListeners()
    {
        inputManager.Cursor.Movement.performed -= OnMovement;
        inputManager.Cursor.Movement.Disable();

        inputManager.Cursor.Interaction.performed -= OnInteraction;
        inputManager.Cursor.Interaction.Disable();

        inputManager.Cursor.Cancel.performed -= OnCancel;
        inputManager.Cursor.Cancel.Disable();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected virtual void OnMovement(InputAction.CallbackContext context)
    {
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected virtual void OnInteraction(InputAction.CallbackContext context)
    {
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected virtual void OnCancel(InputAction.CallbackContext context)
    {
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="targetPosition">TODO: comments</param>
    protected virtual void SelectTile(Vector2 targetPosition)
    {
        if (targetPosition == position || Board.GetTile(targetPosition) is null) return;

        position = targetPosition;
        owner.currentSelectedWorldTile = Board.GetTile(targetPosition);
        tileSelectionCursor.localPosition = currentSelectedWorldTile.position;
    }

    protected virtual Unit GetUnit(Vector2 unitPosition)
    {
        WorldTile t = Board.GetTile(unitPosition);
        GameObject content = t?.content;
        return content != null ? content.GetComponent<Unit>() : null;
    }

    protected virtual void RefreshPrimaryStatPanel(Vector2 unitPosition)
    {
        Unit target = GetUnit(unitPosition);
        if (target != null)
        {
            statPanelController.ShowPrimary(target.gameObject);
        }
        else
        {
            statPanelController.HidePrimary();
        }
    }

    protected virtual void RefreshSecondaryStatPanel(Vector2 unitPosition)
    {
        Unit target = GetUnit(unitPosition);
        if (target != null)
        {
            statPanelController.ShowSecondary(target.gameObject);
        }
        else
        {
            statPanelController.HideSecondary();
        }
    }

    protected virtual bool DidPlayerWin()
    {
        return owner.GetComponent<BaseVictoryCondition>().Victory == Alliances.Hero;
    }

    protected virtual bool IsBattleOver()
    {
        return owner.GetComponent<BaseVictoryCondition>().Victory != Alliances.None;
    }
}