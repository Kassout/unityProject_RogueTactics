using System.Collections.Generic;
using UnityEngine;

public class UnitAbilityArea : AbilityArea
{
    public override List<WorldTile> GetTilesInArea(Vector2 position)
    {
        List<WorldTile> retValue = new List<WorldTile>();
        WorldTile worldTile = Board.GetTile(position);
        if (worldTile != null)
        {
            retValue.Add(worldTile);
        }

        return retValue;
    }
}
