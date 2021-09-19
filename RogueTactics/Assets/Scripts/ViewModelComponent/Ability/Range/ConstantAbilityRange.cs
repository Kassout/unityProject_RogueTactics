using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class ConstantAbilityRange : AbilityRange
{
    public override List<WorldTile> GetTilesInRange()
    {
        return Board.Instance.Search(unit.tile, ExpandSearch);
    }
    
    public override List<WorldTile> GetTilesInRange(List<WorldTile> movementRadiusTiles)
    {
        List<WorldTile> tileInRange = new List<WorldTile>();
        foreach (var tile in movementRadiusTiles)
        {
            tileInRange.AddRange(Board.Instance.Search(tile, ExpandSearch).Where(currentTile => 
                !tileInRange.Any(tile => currentTile.position.Equals(tile.position))));
        }

        return tileInRange;
    }

    private bool ExpandSearch(WorldTile from, WorldTile to)
    {
        return (from.distanceFromStartTile + 1) <= range;
    }
}
