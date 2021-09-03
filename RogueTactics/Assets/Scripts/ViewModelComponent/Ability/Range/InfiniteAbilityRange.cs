using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class InfiniteAbilityRange : AbilityRange
{
    public override List<TileDefinitionData> GetTilesInRange()
    {
        return new List<TileDefinitionData>(Board.tileBoard);
    }

    public override List<TileDefinitionData> GetTilesInRange(List<TileDefinitionData> movementRadiusTiles)
    {
        return new List<TileDefinitionData>(Board.tileBoard);
    }
}
