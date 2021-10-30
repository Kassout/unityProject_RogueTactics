using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// TODO: comments
/// </summary>
public class BattleState : State
{
    #region Fields / Properties
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    protected InputManager inputManager;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected Driver driver;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    protected BattleController owner;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected Camera BattleCamera => owner.battleCamera;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected Transform TileSelectionCursor => owner.tileSelectionCursor;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected WorldTile CurrentSelectedWorldTile => owner.currentSelectedWorldTile;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    protected AbilityMenuPanelController AbilityMenuPanelController => owner.abilityMenuPanelController;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected StatPanelController StatPanelController => owner.statPanelController;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected HitSuccessIndicator HitSuccessIndicator => owner.hitSuccessIndicator;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected Turn Turn => owner.turn;

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected List<Unit> Units => owner.units;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    private Vector2 Position
    {
        get => owner.position;
        set => owner.position = value;
    }

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
        inputManager = new InputManager();
    }

    #endregion

    #region Protected

    /// <summary>
    /// TODO: comments
    /// </summary>
    protected override void AddListeners()
    {
        if (Turn.currentDriver == Drivers.None || Turn.currentDriver == Drivers.Human)
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
    /// TODO: comments
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
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected virtual void OnMovement(InputAction.CallbackContext context) {}

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected virtual void OnInteraction(InputAction.CallbackContext context) {}

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected virtual void OnCancel(InputAction.CallbackContext context) {}

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="targetPosition">TODO: comments</param>
    protected virtual void SelectTile(Vector2 targetPosition)
    {
        if (targetPosition == Position || Board.GetTile(targetPosition) is null) return;

        Position = targetPosition;
        owner.currentSelectedWorldTile = Board.GetTile(targetPosition);
        TileSelectionCursor.localPosition = CurrentSelectedWorldTile.position;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="unitPosition">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    protected virtual Unit GetUnit(Vector2 unitPosition)
    {
        WorldTile t = Board.GetTile(unitPosition);
        GameObject content = t?.content;
        return content != null ? content.GetComponent<Unit>() : null;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="unitPosition">TODO: comments</param>
    protected virtual void RefreshPrimaryStatPanel(Vector2 unitPosition)
    {
        Unit target = GetUnit(unitPosition);
        if (target != null)
        {
            StatPanelController.ShowPrimary(target.gameObject);
        }
        else
        {
            StatPanelController.HidePrimary();
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="unitPosition">TODO: comments</param>
    protected virtual void RefreshSecondaryStatPanel(Vector2 unitPosition)
    {
        Unit target = GetUnit(unitPosition);
        if (target != null)
        {
            StatPanelController.ShowSecondary(target.gameObject);
        }
        else
        {
            StatPanelController.HideSecondary();
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    protected virtual bool DidPlayerWin()
    {
        return owner.GetComponent<BaseVictoryCondition>().Victory == Alliances.Hero;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    protected virtual bool IsBattleOver()
    {
        return owner.GetComponent<BaseVictoryCondition>().Victory != Alliances.None;
    }

    #endregion

    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Enter ()
    {
        driver = (Turn.actor != null) ? Turn.actor.GetComponent<Driver>() : null;
        base.Enter ();
    }

    #endregion
}