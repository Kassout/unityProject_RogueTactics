using System.Collections.Generic;

public class SelfAbilityRange : AbilityRange
{
    public override bool positionOriented => false;
    
    public override List<WorldTile> GetTilesInRange()
    {
        List<WorldTile> retValue = new List<WorldTile>();
        retValue.Add(unit.tile);
        return retValue;
    }

    public override List<WorldTile> GetTilesInRange(List<WorldTile> movementRadiusTiles)
    {
        throw new System.NotImplementedException();
    }
}
