using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MoveTargetState : BattleState
{
    List<Tile> tiles;

    public override void Enter ()
    {
        base.Enter ();
        UnitMovement mover = owner.currentUnit.GetComponent<UnitMovement>();
        tiles = mover.GetTilesInRange(board);
        board.SelectTiles(tiles);
    }
  
    public override void Exit ()
    {
        base.Exit ();
        board.DeSelectTiles(tiles);
        tiles = null;
    }
    
    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        tileSelectionCursor.position = new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
    }

    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        if (tiles.Contains(owner.CurrentTile))
        owner.ChangeState<MoveSequenceState>();
    }
}
