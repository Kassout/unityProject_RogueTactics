using System.Collections.Generic;
using UnityEngine;

public class SpecifyAbilityArea : AbilityArea
{
    public int horizontal;
    private WorldTile _worldTile;

    public override List<WorldTile> GetTilesInArea(Vector2 position)
    {
        _worldTile = Board.GetTile(position);
        return Board.Instance.Search(_worldTile, ExpandSearch);
    }

    private bool ExpandSearch(WorldTile from, WorldTile to)
    {
        return (from.distanceFromStartTile + 1) <= horizontal;
    }
}
