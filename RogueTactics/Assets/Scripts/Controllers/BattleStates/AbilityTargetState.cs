using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityTargetState : BattleState
{
    private List<TileDefinitionData> abilityRangeTiles;

    private List<TileDefinitionData> targetAreaTiles;
    
    private AbilityRange ar;

    private AbilityArea aa;

    public override void Enter()
    {
        base.Enter();
        ar = turn.ability.GetComponent<AbilityRange>();
        aa = turn.ability.GetComponent<AbilityArea>();
        SelectTiles();
    }

    public override void Exit()
    {
        base.Exit();
        Board.Instance.DeSelectTiles(abilityRangeTiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
    }

    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        if (abilityRangeTiles.FindIndex(tile =>
                tile.position.Equals(
                    new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y)))) >=
            0)
        {
            tileSelectionCursor.position =
                new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
            targetAreaTiles = aa.GetTilesInArea(tileSelectionCursor.position);
            Board.Instance.SelectAbilityTiles(abilityRangeTiles);
            Board.Instance.SelectAreaTargetTiles(targetAreaTiles);
        }
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        if (abilityRangeTiles.Contains(Board.GetTile(tileSelectionCursor.position)))
        {
            owner.ChangeState<ConfirmAbilityTargetState>();
        }
        else
        {
            owner.ChangeState<CommandSelectionState>();
        }
    }
    
    protected override void OnCancel(InputAction.CallbackContext context)
    {
        owner.ChangeState<CommandSelectionState>();
    }

    
    void SelectTiles ()
    {
        abilityRangeTiles = ar.GetTilesInRange();
        Board.Instance.SelectAbilityTiles(abilityRangeTiles);
    }
}
