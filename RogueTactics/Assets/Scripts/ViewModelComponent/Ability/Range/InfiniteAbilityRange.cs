using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class InfiniteAbilityRange : AbilityRange
{
    public override bool positionOriented => false;
    
    public override List<WorldTile> GetTilesInRange()
    {
        return new List<WorldTile>(Board.tileBoard);
    }

    public override List<WorldTile> GetTilesInRange(List<WorldTile> movementRadiusTiles)
    {
        return new List<WorldTile>(Board.tileBoard);
    }
}
