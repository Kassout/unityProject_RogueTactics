using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityTargetState : BattleState
{
    private List<TileDefinitionData> tiles;

    private AbilityRange ar;

    public override void Enter()
    {
        base.Enter();
        ar = turn.ability.GetComponent<AbilityRange>();
        SelectTiles();
    }

    public override void Exit()
    {
        base.Exit();
        Board.Instance.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
    }

    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        if (tiles.FindIndex(tile =>
                tile.position.Equals(
                    new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y)))) >=
            0)
            tileSelectionCursor.position =
                new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        if (turn.hasUnitMoved)
            turn.lockMove = true;
    }
    
    protected override void OnCancel(InputAction.CallbackContext context)
    {
        owner.ChangeState<CommandSelectionState>();
    }

    
    void SelectTiles ()
    {
        tiles = ar.GetTilesInRange();
        Board.Instance.SelectAbilityTiles(tiles);
    }
}
