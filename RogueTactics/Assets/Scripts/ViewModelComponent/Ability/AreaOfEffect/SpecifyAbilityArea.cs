using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecifyAbilityArea : AbilityArea
{
    public int horizontal;
    private TileDefinitionData tile;

    public override List<TileDefinitionData> GetTilesInArea(Vector2 position)
    {
        tile = Board.GetTile(position);
        return Board.Instance.Search(tile, horizontal, ExpandSearch);
    }

    private bool ExpandSearch(TileDefinitionData from, TileDefinitionData to)
    {
        return (from.distanceFromStartTile + 1) <= horizontal;
    }
}
