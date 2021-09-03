using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private List<TileDefinitionData> _movableTiles;

        private List<TileDefinitionData> _actionableTiles;

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public override void Enter()
        {
            base.Enter();
            var mover = owner.turn.actor.GetComponent<UnitMovement>();
            _movableTiles = mover.GetTilesInRange();
            _movableTiles.Add(owner.turn.actor.TileDefinition);
            Board.Instance.SelectTiles(_movableTiles);
            
            var ability = turn.actor.GetComponentInChildren<AbilityRange>();
            if (ability != null)
            {
                turn.ability = ability.gameObject;
                
                List<TileDefinitionData> boundTiles = ComputeMovementBoundTiles(_movableTiles);
                _actionableTiles = ability.GetTilesInRange(boundTiles);

                foreach (var movableTile in _movableTiles)
                {
                    if (_actionableTiles.Any(tile => tile.position.Equals(movableTile.position)))
                    {
                        _actionableTiles.RemoveAll(tile => tile.position.Equals(movableTile.position));
                    }
                }
                
                Board.Instance.SelectAbilityTiles(_actionableTiles);
            }
        }

        private List<TileDefinitionData> ComputeMovementBoundTiles(List<TileDefinitionData> movableRadiusTiles)
        {
            List<TileDefinitionData> boundTiles = new List<TileDefinitionData>();

            int minX = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.x < t2.position.x ? t1 : t2).position.x;
            int minY = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.y < t2.position.y ? t1 : t2).position.y;
            int maxX = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.x > t2.position.x ? t1 : t2).position.x;
            int maxY = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.y > t2.position.y ? t1 : t2).position.y;
            
            for (int x = minX; x <= maxX; x++)
            {
                var x1 = x;
                List<TileDefinitionData> results = movableRadiusTiles.GroupBy(tile => tile.position)
                    .Where(group => group.All(tile => tile.position.x.Equals(x1))).Select(group => group.First()).ToList();

                TileDefinitionData maxTile = results.Aggregate((t1, t2) => t1.position.y > t2.position.y ? t1 : t2);
                TileDefinitionData minTile = results.Aggregate((t1, t2) => t1.position.y < t2.position.y ? t1 : t2);
                
                if (!boundTiles.Contains(maxTile))
                {
                    boundTiles.Add(maxTile);
                }

                if (!boundTiles.Contains(minTile))
                {
                    boundTiles.Add(minTile);
                }
            }
            
            
            for (int y = minY; y <= maxY; y++)
            {
                var y1 = y;
                List<TileDefinitionData> results = movableRadiusTiles.GroupBy(tile => tile.position)
                    .Where(group => group.All(tile => tile.position.y.Equals(y1))).Select(group => group.First()).ToList();

                TileDefinitionData maxTile = results.Aggregate((t1, t2) => t1.position.x > t2.position.x ? t1 : t2);
                TileDefinitionData minTile = results.Aggregate((t1, t2) => t1.position.x < t2.position.x ? t1 : t2);
                
                if (!boundTiles.Contains(maxTile))
                {
                    boundTiles.Add(maxTile);
                }

                if (!boundTiles.Contains(minTile))
                {
                    boundTiles.Add(minTile);
                }
            }

            return boundTiles;
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        public override void Exit()
        {
            base.Exit();
            Board.Instance.DeSelectTiles(_movableTiles);
            Board.Instance.DeSelectTiles(_actionableTiles);
            _movableTiles = null;
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="context">TODO: comments</param>
        protected override void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 mouseScreenPos = battleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            
            if (_movableTiles.Any(tile =>
                    tile.position.Equals(new Vector2(Mathf.RoundToInt(mouseScreenPos.x),
                        Mathf.RoundToInt(mouseScreenPos.y))))
                || _actionableTiles.Any(tile =>
                    tile.position.Equals(new Vector2(Mathf.RoundToInt(mouseScreenPos.x),
                        Mathf.RoundToInt(mouseScreenPos.y)))))
            {
                tileSelectionCursor.position = new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
            }
        }

        /// <summary>
        ///     TODO: comments
        /// </summary>
        /// <param name="context">TODO: comments</param>
        protected override void OnInteraction(InputAction.CallbackContext context)
        {
            if (_movableTiles.FindIndex(tile => tile.position.Equals(tileSelectionCursor.position)) >= 0)
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