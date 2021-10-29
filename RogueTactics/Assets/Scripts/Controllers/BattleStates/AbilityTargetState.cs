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
        Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        if (_abilityRangeTiles.FindIndex(tile =>
                tile.position.Equals(
                    new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y)))) >=
            0)
        {
            tileSelectionCursor.position =
                new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
            _targetAreaTiles = _abilityArea.GetTilesInArea(tileSelectionCursor.position);
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
        if (_abilityRangeTiles.Contains(Board.GetTile(tileSelectionCursor.position)))
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

    #region Public

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        _abilityRange = turn.ability.GetComponent<AbilityRange>();
        _abilityArea = turn.ability.GetComponent<AbilityArea>();

        SelectTiles();

        if (_driver.Current == Drivers.Computer)
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
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
    }

    #endregion

    #region Private

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <returns>TODO: comments</returns>
    private IEnumerator ComputerHighlightTarget()
    {
        Vector2 cursorPos = tileSelectionCursor.position;
        while (cursorPos != turn.plan.fireLocation)
        {
            if (cursorPos.x < turn.plan.fireLocation.x)
            {
                cursorPos.x++;
            }

            if (cursorPos.x > turn.plan.fireLocation.x)
            {
                cursorPos.x--;
            }

            if (cursorPos.y < turn.plan.fireLocation.y)
            {
                cursorPos.y++;
            }

            if (cursorPos.y > turn.plan.fireLocation.y)
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
}