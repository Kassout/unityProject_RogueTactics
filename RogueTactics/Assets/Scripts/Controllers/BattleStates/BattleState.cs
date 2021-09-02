using System.Collections.Generic;
using Common.StateMachine;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     TODO: comments
/// </summary>
public class BattleState : State
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    private InputManager _inputManager;

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
    protected TileDefinitionData currentSelectedTile => owner.currentSelectedTile;

    public AbilityMenuPanelController abilityMenuPanelController => owner.abilityMenuPanelController;

    public StatPanelController statPanelController => owner.statPanelController;

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
        _inputManager = new InputManager();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected override void AddListeners()
    {
        _inputManager.Cursor.Movement.performed += OnMovement;
        _inputManager.Cursor.Movement.Enable();

        _inputManager.Cursor.Interaction.performed += OnInteraction;
        _inputManager.Cursor.Interaction.Enable();

        _inputManager.Cursor.Cancel.performed += OnCancel;
        _inputManager.Cursor.Cancel.Enable();
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    protected override void RemoveListeners()
    {
        _inputManager.Cursor.Movement.performed -= OnMovement;
        _inputManager.Cursor.Movement.Disable();

        _inputManager.Cursor.Interaction.performed -= OnInteraction;
        _inputManager.Cursor.Interaction.Disable();

        _inputManager.Cursor.Cancel.performed -= OnCancel;
        _inputManager.Cursor.Cancel.Disable();
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
        tileSelectionCursor.localPosition = Board.GetTile(targetPosition).position;
    }

    protected virtual Unit GetUnit(Vector2 unitPosition)
    {
        TileDefinitionData t = Board.GetTile(unitPosition);
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
}