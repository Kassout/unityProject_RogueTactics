using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// TODO: comments
/// </summary>
public class AbilityTargetState : BattleState
{
    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    private List<WorldTile> _abilityRangeTiles;

    /// <summary>
    /// TODO: comments
    /// </summary>
    private List<WorldTile> _targetAreaTiles;

    /// <summary>
    /// TODO: comments
    /// </summary>
    private AbilityRange _abilityRange;

    /// <summary>
    /// TODO: comments
    /// </summary>
    private AbilityArea _abilityArea;

    #endregion

    #region InputSystem

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = BattleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        if (_abilityRangeTiles.FindIndex(tile =>
                tile.position.Equals(
                    new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y)))) >=
            0)
        {
            TileSelectionCursor.position =
                new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
            _targetAreaTiles = _abilityArea.GetTilesInArea(TileSelectionCursor.position);
            Board.Instance.SelectAbilityTiles(_abilityRangeTiles);
            Board.Instance.SelectAreaTargetTiles(_targetAreaTiles);
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        if (_abilityRangeTiles.Contains(Board.GetTile(TileSelectionCursor.position)))
        {
            owner.ChangeState<ConfirmAbilityTargetState>();
        }
        else
        {
            owner.ChangeState<CommandSelectionState>();
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnCancel(InputAction.CallbackContext context)
    {
        owner.ChangeState<CommandSelectionState>();
    }

    #endregion

    #region Private

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    private IEnumerator ComputerHighlightTarget()
    {
        Vector2 cursorPos = TileSelectionCursor.position;
        while (cursorPos != Turn.plan.fireLocation)
        {
            if (cursorPos.x < Turn.plan.fireLocation.x)
            {
                cursorPos.x++;
            }

            if (cursorPos.x > Turn.plan.fireLocation.x)
            {
                cursorPos.x--;
            }

            if (cursorPos.y < Turn.plan.fireLocation.y)
            {
                cursorPos.y++;
            }

            if (cursorPos.y > Turn.plan.fireLocation.y)
            {
                cursorPos.y--;
            }

            SelectTile(cursorPos);
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.5f);
        owner.ChangeState<ConfirmAbilityTargetState>();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    private void SelectTiles()
    {
        _abilityRangeTiles = _abilityRange.GetTilesInRange();
        Board.Instance.SelectAbilityTiles(_abilityRangeTiles);
    }

    #endregion
    
    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        _abilityRange = Turn.ability.GetComponent<AbilityRange>();
        _abilityArea = Turn.ability.GetComponent<AbilityArea>();

        SelectTiles();

        if (driver.Current == Drivers.Computer)
        {
            StartCoroutine(ComputerHighlightTarget());
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        Board.Instance.DeSelectTiles(_abilityRangeTiles);
        StatPanelController.HidePrimary();
        StatPanelController.HideSecondary();
    }

    #endregion
}