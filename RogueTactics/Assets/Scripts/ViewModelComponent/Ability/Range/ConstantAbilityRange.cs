using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

public class ConstantAbilityRange : AbilityRange
{
    public override List<TileDefinitionData> GetTilesInRange()
    {
        return Board.Instance.Search(unit.TileDefinition, horizontal, ExpandSearch);
    }
    
    public override List<TileDefinitionData> GetTilesInRange(List<TileDefinitionData> movementRadiusTiles)
    {
        List<TileDefinitionData> tileInRange = new List<TileDefinitionData>();
        foreach (var tile in movementRadiusTiles)
        {
            tileInRange.AddRange(Board.Instance.Search(tile, horizontal, ExpandSearch).Where(currentTile => !tileInRange.Any(tile => currentTile.position.Equals(tile.position))));
        }

        return tileInRange;
    }

    private bool ExpandSearch(TileDefinitionData from, TileDefinitionData to)
    {
        return (from.distanceFromStartTile + 1) <= horizontal;
    }
}
