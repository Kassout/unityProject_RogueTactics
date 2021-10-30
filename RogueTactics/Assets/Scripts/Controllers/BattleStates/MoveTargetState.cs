using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// TODO: comments
/// </summary>
public class MoveTargetState : BattleState
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    private List<WorldTile> _movableTiles;

    private List<WorldTile> _actionableTiles;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        
        Cursor.visible = false;
        
        if (Turn.currentDriver == Drivers.Computer)
        {
            StartCoroutine(ComputerHighlightMoveTarget());
        }
        else
        {
            var mover = owner.turn.actor.GetComponent<UnitMovement>();
            _movableTiles = mover.GetTilesInRange();
            _movableTiles.Add(owner.turn.actor.tile);
            Board.Instance.SelectTiles(_movableTiles);
            
            var ability = Turn.actor.GetComponentInChildren<AbilityRange>();
            if (ability != null)
            {
                List<WorldTile> boundTiles = ComputeMovementBoundTiles(_movableTiles);
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
    }

    private IEnumerator ComputerHighlightMoveTarget()
    {
        Vector2 cursorPos = TileSelectionCursor.position;
        while (cursorPos != Turn.plan.moveLocation)
        {
            if (cursorPos.x < Turn.plan.moveLocation.x) cursorPos.x++;
            if (cursorPos.x > Turn.plan.moveLocation.x) cursorPos.x--;
            if (cursorPos.y < Turn.plan.moveLocation.y) cursorPos.y++;
            if (cursorPos.y > Turn.plan.moveLocation.y) cursorPos.y--;
            SelectTile(cursorPos);
        }

        yield return new WaitForSeconds(0.5f);
        owner.ChangeState<MoveSequenceState>();
    }

    private List<WorldTile> ComputeMovementBoundTiles(List<WorldTile> movableRadiusTiles)
    {
        List<WorldTile> boundTiles = new List<WorldTile>();

        int minX = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.x < t2.position.x ? t1 : t2).position.x;
        int minY = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.y < t2.position.y ? t1 : t2).position.y;
        int maxX = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.x > t2.position.x ? t1 : t2).position.x;
        int maxY = (int) movableRadiusTiles.Aggregate((t1, t2) => t1.position.y > t2.position.y ? t1 : t2).position.y;
            
        for (int x = minX; x <= maxX; x++)
        {
            var x1 = x;
            List<WorldTile> results = movableRadiusTiles.GroupBy(tile => tile.position)
                .Where(group => group.All(tile => tile.position.x.Equals(x1))).Select(group => group.First()).ToList();

            WorldTile maxWorldTile = results.Aggregate((t1, t2) => t1.position.y > t2.position.y ? t1 : t2);
            WorldTile minWorldTile = results.Aggregate((t1, t2) => t1.position.y < t2.position.y ? t1 : t2);
                
            if (!boundTiles.Contains(maxWorldTile))
            {
                boundTiles.Add(maxWorldTile);
            }

            if (!boundTiles.Contains(minWorldTile))
            {
                boundTiles.Add(minWorldTile);
            }
        }
            
            
        for (int y = minY; y <= maxY; y++)
        {
            var y1 = y;
            List<WorldTile> results = movableRadiusTiles.GroupBy(tile => tile.position)
                .Where(group => group.All(tile => tile.position.y.Equals(y1))).Select(group => group.First()).ToList();

            WorldTile maxWorldTile = results.Aggregate((t1, t2) => t1.position.x > t2.position.x ? t1 : t2);
            WorldTile minWorldTile = results.Aggregate((t1, t2) => t1.position.x < t2.position.x ? t1 : t2);
                
            if (!boundTiles.Contains(maxWorldTile))
            {
                boundTiles.Add(maxWorldTile);
            }

            if (!boundTiles.Contains(minWorldTile))
            {
                boundTiles.Add(minWorldTile);
            }
        }

        return boundTiles;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        Cursor.visible = true;
        if (Turn.currentDriver != Drivers.Computer)
        {
            Board.Instance.DeSelectTiles(_movableTiles);
            Board.Instance.DeSelectTiles(_actionableTiles);
            _movableTiles = null;
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = BattleCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            
        if (_movableTiles.Any(tile =>
                tile.position.Equals(new Vector2(Mathf.RoundToInt(mouseScreenPos.x),
                    Mathf.RoundToInt(mouseScreenPos.y))))
            || _actionableTiles.Any(tile =>
                tile.position.Equals(new Vector2(Mathf.RoundToInt(mouseScreenPos.x),
                    Mathf.RoundToInt(mouseScreenPos.y)))))
        {
            TileSelectionCursor.position = new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnInteraction(InputAction.CallbackContext context)
    {
        if (_movableTiles.FindIndex(tile => tile.position.Equals(TileSelectionCursor.position)) >= 0)
        {
            owner.currentSelectedWorldTile = Board.GetTile(TileSelectionCursor.position);
            owner.ChangeState<MoveSequenceState>();
        } 
        if (TileSelectionCursor.position.Equals(owner.turn.actor.tile.position))
        {
            owner.currentSelectedWorldTile = Board.GetTile(TileSelectionCursor.position);
            owner.ChangeState<CommandSelectionState>();
        }

        if (_actionableTiles.FindIndex(tile => tile.position.Equals(TileSelectionCursor.position)) >= 0 && Board.GetTile(TileSelectionCursor.position).content)
        {
            owner.currentSelectedWorldTile = Board.GetTile(TileSelectionCursor.position);
            Turn.ability = Turn.actor.GetComponentInChildren<Ability>();
            Turn.targets = new List<WorldTile> { owner.currentSelectedWorldTile };
            owner.ChangeState<MoveSequenceState>();
        }
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    protected override void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel unit movement");
        owner.ChangeState<SelectUnitState>();
    }
}