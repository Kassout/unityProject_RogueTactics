using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilityArea : AbilityArea
{
    public override List<TileDefinitionData> GetTilesInArea(Vector2 position)
    {
        List<TileDefinitionData> retValue = new List<TileDefinitionData>();
        TileDefinitionData tile = Board.GetTile(position);
        if (tile != null)
        {
            retValue.Add(tile);
        }

        return retValue;
    }
}
