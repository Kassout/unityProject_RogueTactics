using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class SelfAbilityRange : AbilityRange
{
    public override bool positionOriented => false;
    
    public override List<TileDefinitionData> GetTilesInRange()
    {
        List<TileDefinitionData> retValue = new List<TileDefinitionData>();
        retValue.Add(unit.TileDefinition);
        return retValue;
    }

    public override List<TileDefinitionData> GetTilesInRange(List<TileDefinitionData> movementRadiusTiles)
    {
        throw new System.NotImplementedException();
    }
}
