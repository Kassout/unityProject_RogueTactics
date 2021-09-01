using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;
using ViewModelComponent;

namespace BattleStates
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public class MoveTargetState : BattleState
    {
        /// <summary>
        ///     TODO: comments
        /// </summary>
        private List<TileDefinitionData> _tiles;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public override void Enter()
        {
            base.Enter();
            var mover = owner.turn.actor.GetComponent<UnitMovement>();
            _tiles = mover.GetTilesInRange();
            Board.Instance.SelectTiles(_tiles);
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public override void Exit()
        {
            base.Exit();
            Board.Instance.DeSelectTiles(_tiles);
            _tiles = null;
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="context">TODO: comments</param>
        protected override void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            if (_tiles.FindIndex(tile =>
                    tile.position.Equals(
                        new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y)))) >=
                0)
                tileSelectionCursor.position =
                    new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="context">TODO: comments</param>
        protected override void OnInteraction(InputAction.CallbackContext context)
        {
            if (_tiles.FindIndex(tile => tile.position.Equals(tileSelectionCursor.position)) >= 0)
            {
                owner.currentSelectedTile = Board.GetTile(tileSelectionCursor.position);
                owner.ChangeState<MoveSequenceState>();
            } 
            else if (tileSelectionCursor.position.Equals(owner.turn.actor.TileDefinition.position))
            {
                owner.currentSelectedTile = Board.GetTile(tileSelectionCursor.position);
                owner.ChangeState<CommandSelectionState>();
            }
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="context">TODO: comments</param>
        protected override void OnCancel(InputAction.CallbackContext context)
        {
            Debug.Log("Cancel unit movement");
            owner.ChangeState<SelectUnitState>();
        }
    }
}