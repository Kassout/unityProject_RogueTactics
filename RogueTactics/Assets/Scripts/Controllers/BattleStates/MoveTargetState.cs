using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;
using ViewModelComponent;

namespace BattleStates
{
    public class MoveTargetState : BattleState
    {
        private List<TileDefinitionData> _tiles;

        public override void Enter()
        {
            base.Enter();
            UnitMovement mover = owner.currentUnit.GetComponent<UnitMovement>();
            _tiles = mover.GetTilesInRange();
            Board.Instance.SelectTiles(_tiles);
        }
  
        public override void Exit ()
        {
            base.Exit ();
            Board.Instance.DeSelectTiles(_tiles);
            _tiles = null;
        }
    
        protected override void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            if (_tiles.FindIndex(tile => tile.position.Equals(new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y)))) >= 0)
            {
                tileSelectionCursor.position = new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
            }
        }

        protected override void OnInteraction(InputAction.CallbackContext context)
        {
            if (_tiles.FindIndex(tile => tile.position.Equals(tileSelectionCursor.position)) >= 0)
            {
                owner.currentTile = Board.GetTile(tileSelectionCursor.position);
                owner.ChangeState<MoveSequenceState>();
            }
        }

        protected override void OnCancel(InputAction.CallbackContext context)
        {
            Debug.Log("Cancel unit movement");
            owner.ChangeState<SelectUnitState>();
        }
    }
}
